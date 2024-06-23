import { PaymentElement } from "@stripe/react-stripe-js";
import { useState, useRef } from "react";
import { useStripe, useElements } from "@stripe/react-stripe-js";
import { useParams } from "react-router-dom";

import { promoteProduct } from "../fetch/requests/products";
import Modal from "../components/Modal";

export default function CheckoutForm() {
  const stripe = useStripe();
  const elements = useElements();

  const modal = useRef();

  const [responseResult, setResponseResult] = useState(null);
  const params = useParams();

  const [message, setMessage] = useState(null);
  const [isProcessing, setIsProcessing] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!stripe || !elements) {
      return;
    }

    setIsProcessing(true);

    const { error, paymentIntent } = await stripe.confirmPayment({
      elements,
      confirmParams: {
        return_url: `${window.location.origin}/completion`,
      },
      redirect: "if_required",
    });

    if (paymentIntent.status === "succeeded") {
      (async function () {
        try {
          var response = await promoteProduct({
            productId: params.productId,
            promotionPricingId: params.promotionId,
          });

          if (response.status !== 200) {
            throw new Error("Network response was not ok");
          }

          setResponseResult("ok");
          modal.current.open();
        } catch (error) {
          setResponseResult("bad");
          modal.current.open();
        }
      })();
    }

    if (error) {
      if (error.type === "card_error" || error.type === "validation_error") {
        setMessage(error.message);
      } else {
        setMessage("An unexpected error occured.");
      }
    }

    setIsProcessing(false);
  };

  function redirectTo() {
    window.location.href = "/products/myProducts";
  }

  return (
    <>
      <Modal
        ref={modal}
        performAction={
          responseResult === "ok"
            ? () => redirectTo("/login")
            : () => redirectTo("/")
        }
        buttonCaption="Okay"
      >
        {responseResult === "ok" && (
          <>
            <h2 className="text-xl font-bold text-green-700 my-4">
              Successful Promotion of your product!
            </h2>
            <p className="text-stone-600 mb-4">
              Now it will show on top of others.
            </p>
          </>
        )}
        {responseResult === "bad" && (
          <>
            <h2 className="text-xl font-bold text-red-700 my-4">
              Promoting failed.
            </h2>
            <p className="text-stone-600 mb-4">Please try again.</p>
          </>
        )}
      </Modal>
      <form id="payment-form" onSubmit={handleSubmit}>
        <PaymentElement id="payment-element" />
        <button
          className="border border-spacing-3 p-2 rounded-md"
          disabled={isProcessing || !stripe || !elements}
          id="submit"
        >
          <span id="button-text">
            {isProcessing ? "Processing ... " : "Pay now"}
          </span>
        </button>
        {/* Show any error or success messages */}
        {message && <div id="payment-message">{message}</div>}
      </form>
    </>
  );
}
