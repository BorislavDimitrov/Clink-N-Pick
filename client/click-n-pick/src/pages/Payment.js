import { useEffect, useState } from "react";
import { loadStripe } from "@stripe/stripe-js";
import { Elements } from "@stripe/react-stripe-js";
import { useParams } from "react-router-dom";

import { createPaymentIntent } from "../fetch/requests/payments";
import CheckoutForm from "./CheckoutForm";

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
        const response = await createPaymentIntent(params.promotionId);

        if (response.status !== 200) {
          throw new Error("Network response was not ok");
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
      {clientSecret && stripePromise && (
        <Elements stripe={stripePromise} options={{ clientSecret }}>
          <CheckoutForm />
        </Elements>
      )}
    </div>
  );
}

export default Payment;
