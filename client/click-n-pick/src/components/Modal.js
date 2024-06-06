import { forwardRef, useImperativeHandle, useRef } from "react";
import { createPortal } from "react-dom";

const Modal = forwardRef(function Modal(
  { children, buttonCaption, performAction },
  ref
) {
  const dialog = useRef();

  useImperativeHandle(ref, () => {
    return {
      open() {
        dialog.current.showModal();
      },
    };
  });

  function handleClick(e) {
    if (typeof performAction === "function") {
      performAction();
    }
  }

  return createPortal(
    <dialog
      ref={dialog}
      className="backdrop:bg-stone-900/90 p-10 rounded-md shadow-md"
    >
      {children}
      <form method="dialog" className="mt-4 text-center">
        <button onClick={handleClick} className=" bg-slate-400 p-2 rounded-md">
          {buttonCaption}
        </button>
      </form>
    </dialog>,
    document.getElementById("modal-root")
  );
});

export default Modal;
