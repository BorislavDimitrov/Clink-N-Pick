import { useParams, useNavigate } from "react-router-dom";
import { useState, useEffect, useRef } from "react";

import { confirmEmail } from "../../fetch/requests/identity";
import Modal from "../../components/Modal";

function ConfirmEmail() {
  const params = useParams();
  const modal = useRef();

  const navigate = useNavigate();
  const [responseResult, setResponseResult] = useState(null);

  useEffect(() => {
    (async function sendEmailConfirmation() {
      try {
        const response = await confirmEmail(params);

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
  }, []);

  function redirectTo(route) {
    navigate(route);
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
              Successful Confirmation of your Email!
            </h2>
            <p className="text-stone-600 mb-4">You can now Sign Up.</p>
          </>
        )}
        {responseResult === "bad" && (
          <>
            <h2 className="text-xl font-bold text-red-700 my-4">
              Confirming your email failed!
            </h2>
            <p className="text-stone-600 mb-4">Please try again.</p>
          </>
        )}
      </Modal>
      <div className="max-w-md mx-auto text-center bg-white bg-opacity-70 p-8 rounded-lg shadow-lg mt-20">
        <h1 className="text-xl font-bold text-gray-800 mb-6">
          Email Confirmation in Progress
        </h1>
        <p className="text-lg text-grey-900 mb-8">
          Thank you for verifying your email address. Your email confirmation is
          currently being processed. This should only take a moment.
        </p>
      </div>
    </>
  );
}

export default ConfirmEmail;
