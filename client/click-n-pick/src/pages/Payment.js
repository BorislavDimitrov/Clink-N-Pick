import { useEffect, useState } from "react";
import { loadStripe } from "@stripe/stripe-js";
import { Elements } from "@stripe/react-stripe-js";
import CheckoutForm from "./CheckoutForm";
import { useParams } from "react-router-dom";
import { createPaymentIntent } from "../fetch/requests/payments";

function Payment() {
  const params = useParams();

  const [stripePromise, setStripePromise] = useState(
    loadStripe(
      "pk_test_51PKy7DC7YySDPOUFfTy97gIQd4Wk2BWEzIB7msdnBr2Lo6C7GangYaFkZSkHk8ls8yQ1pVMf9hZAEhrtODdiJMWW00IIl7LpzR"
    )
  );
  const [clientSecret, setClientSecret] = useState("");

  useEffect(() => {
    (async function createPayment() {
      try {
        console.log(params);
        const response = await createPaymentIntent(params.promotionId);

        if (response.status !== 200) {
          throw new Error("Payment failed");
        }

        var data = await response.json();
        setClientSecret(data.clientSecret);
      } catch (error) {
        alert("Some error occured");
      }
    })();
  }, []);

  return (
    <div className=" w-1/4 mx-auto my-auto">
      <h1>React Stripe and the Payment Element</h1>
      {clientSecret && stripePromise && (
        <Elements stripe={stripePromise} options={{ clientSecret }}>
          <CheckoutForm />
        </Elements>
      )}
    </div>
  );
}

export default Payment;
