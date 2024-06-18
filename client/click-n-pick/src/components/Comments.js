import { useState, useEffect } from "react";
import CommentForm from "./CommentForm";
import Comment from "./Comment";
import {
  createComment,
  getForProduct,
  deleteComment,
} from "../fetch/requests/comments";

const Comments = ({ currentUserId, productId }) => {
  const [comments, setComments] = useState([]);
  const [activeComment, setActiveComment] = useState(null);
  const rootComments = comments.filter((comment) => comment.parentId === null);

  const getReplies = (commentId) =>
    comments
      .filter((backendComment) => backendComment.parentId === commentId)
      .sort(
        (a, b) =>
          new Date(a.createdAt).getTime() - new Date(b.createdAt).getTime()
      );

  async function addComment(text, parentId) {
    console.log(text);
    var response = await createComment({
      Content: text,
      ParentId: parentId,
      ProductId: productId,
    });
    var data = await response.json();
    console.log(data);
    setComments([data, ...comments]);
    setActiveComment(null);
  }

  async function deleteCom(commentId) {
    if (window.confirm("Are you sure you want to remove comment?")) {
      try {
        const response = await deleteComment(commentId);

        if (response.status !== 200) {
          throw new Error("Fetching details failed.");
        }

        const updatedComments = comments.filter(
          (backendComment) => backendComment.id !== commentId
        );
        setComments(updatedComments);
      } catch (error) {
        alert("Some problem occurred.");
      }
    }
  }

  useEffect(() => {
    (async function () {
      try {
        const response = await getForProduct(productId);
        if (response.status !== 200) {
          throw new Error("Fetching details failed.");
        }

        const data = await response.json();

        console.log(data);
        setComments(data.comments);
      } catch (error) {
        alert("Some problem occurred.");
      }
    })();
  }, []);

  return (
    <div class="antialiased mx-auto max-w-screen-sm">
      <h3 class="mb-4 text-lg font-semibold text-gray-900">Comments</h3>

      <CommentForm submitLabel="Write" handleSubmit={addComment} />
      <div className="comments-container">
        {rootComments.map((rootComment) => (
          <Comment
            key={rootComment.id}
            comment={rootComment}
            replies={getReplies(rootComment.id)}
            activeComment={activeComment}
            setActiveComment={setActiveComment}
            addComment={addComment}
            deleteComment={deleteCom}
            currentUserId={currentUserId}
          />
        ))}
      </div>
    </div>
  );
};

export default Comments;
