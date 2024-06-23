import { useState } from "react";

const CommentForm = ({
  handleSubmit,
  submitLabel,
  initialText = "",
  hasCancelButton = false,
  handleCancel,
}) => {
  const [text, setText] = useState(initialText);
  const isTextareaDisabled = text.length === 0;
  const onSubmit = (event) => {
    event.preventDefault();
    handleSubmit(text);
    setText("");
  };

  return (
    <form onSubmit={onSubmit} className="mb-6">
      <div className="py-2 px-4 mb-4 bg-white rounded-lg rounded-t-lg border border-gray-200 dark:bg-gray-800 dark:border-gray-700">
        <label for="comment" className="sr-only">
          Your comment
        </label>
        <textarea
          id="comment"
          rows="6"
          className="px-0 w-full text-sm text-gray-900 border-0 focus:ring-0 focus:outline-none dark:text-white dark:placeholder-gray-400 dark:bg-gray-800"
          placeholder="Write a comment..."
          required
          value={text}
          maxLength={2000}
          onChange={(e) => setText(e.target.value)}
        ></textarea>
      </div>
      <button
        disabled={isTextareaDisabled}
        type="submit"
        className="inline-flex items-center py-2.5 px-4 text-xs font-medium text-center text-white bg-blue-700 rounded-lg focus:ring-4 focus:ring-primary-200 dark:focus:ring-primary-900 hover:bg-primary-800"
      >
        {submitLabel}
      </button>

      {hasCancelButton && (
        <button
          type="button"
          className="inline-flex items-center py-2.5 px-4 text-xs font-medium text-center text-white bg-red-700 rounded-lg focus:ring-4 focus:ring-red-200  hover:bg-red-800"
          onClick={handleCancel}
        >
          Cancel
        </button>
      )}
    </form>
  );
};

export default CommentForm;
