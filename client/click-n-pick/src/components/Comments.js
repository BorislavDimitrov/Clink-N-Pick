import { useState, useEffect } from "react";

import { GetAuthToken } from "../Utility/auth";
import CommentForm from "./CommentForm";
import Comment from "./Comment";
import {
  createComment,
  getForProduct,
  deleteComment,
  editComment as editCommentApi,
} from "../fetch/requests/comments";

const Comments = ({ currentUserId, productId }) => {
  const [comments, setComments] = useState([]);
  const [activeComment, setActiveComment] = useState(null);
  const rootComments = comments.filter((comment) => comment.parentId === null);
  const token = GetAuthToken();

  const getReplies = (commentId) =>
    comments
      .filter((backendComment) => backendComment.parentId === commentId)
      .sort(
        (a, b) =>
          new Date(a.createdAt).getTime() - new Date(b.createdAt).getTime()
      );

  const updateComment = async (text, commentId) => {
    try {
      var response = await editCommentApi({
        Content: text,
        CommentId: commentId,
      });

      if (response.status !== 200) {
        throw new Error("Network response was not ok");
      }

      const updatedBackendComments = comments.map((comment) => {
        if (comment.id === commentId) {
          return { ...comment, content: text };
        }
        return comment;
      });

      setComments(updatedBackendComments);
      setActiveComment(null);
    } catch (error) {
      alert("Some problem occurred.");
    }
  };

  const addComment = async (text, parentId) => {
    try {
      var response = await createComment({
        Content: text,
        ParentId: parentId,
        ProductId: productId,
      });

      if (response.status !== 200) {
        throw new Error("Network response was not ok");
      }

      var data = await response.json();
      setComments([data, ...comments]);
      setActiveComment(null);
    } catch (error) {
      alert("Some problem occurred.");
    }
  };

  const removeComment = async (commentId) => {
    if (window.confirm("Are you sure you want to remove comment?")) {
      try {
        const response = await deleteComment(commentId);

        if (response.status !== 200) {
          throw new Error("Network response was not ok");
        }

        const updatedComments = comments.filter(
          (backendComment) => backendComment.id !== commentId
        );
        setComments(updatedComments);
      } catch (error) {
        alert("Some problem occurred.");
      }
    }
  };

  useEffect(() => {
    (async function () {
      try {
        const response = await getForProduct(productId);

        if (response.status !== 200) {
          throw new Error("Network response was not ok");
        }

        const data = await response.json();

        setComments(data.comments);
      } catch (error) {
        alert("Some problem occurred.");
      }
    })();
  }, []);

  return (
    <div className="antialiased mx-auto max-w-screen-sm mt-10">
      <h3 className="mb-4 text-lg font-semibold text-gray-900">Comments</h3>

      {token && <CommentForm submitLabel="Post" handleSubmit={addComment} />}
      <div className="comments-container">
        {rootComments.map((rootComment) => (
          <Comment
            key={rootComment.id}
            comments={comments}
            comment={rootComment}
            replies={getReplies(rootComment.id)}
            activeComment={activeComment}
            setActiveComment={setActiveComment}
            updateComment={updateComment}
            addComment={addComment}
            removeComment={removeComment}
            currentUserId={currentUserId}
          />
        ))}
      </div>
    </div>
  );
};

export default Comments;
