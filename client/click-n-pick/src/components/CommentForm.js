import { useState } from "react";

const CommentForm = ({ handleSubmit, submitLabel, initialText = "" }) => {
  const [text, setText] = useState(initialText);
  const isTextareaDisabled = text.length === 0;
  const onSubmit = (event) => {
    event.preventDefault();
    console.log(text);
    handleSubmit(text);
    setText("");
  };
  return (
    <form onSubmit={onSubmit}>
      <textarea
        className="border-4"
        value={text}
        onChange={(e) => setText(e.target.value)}
      />
      <button
        type="submit"
        class="focus:outline-none text-white bg-green-700 hover:bg-green-800 focus:ring-4 focus:ring-green-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-green-600 dark:hover:bg-green-700 dark:focus:ring-green-800"
        disabled={isTextareaDisabled}
      >
        {submitLabel}
      </button>
    </form>
  );
};

export default CommentForm;
