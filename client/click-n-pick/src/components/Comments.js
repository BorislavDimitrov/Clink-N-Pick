import { useState, useEffect } from "react";
import CommentForm from "./CommentForm";
import Comment from "./Comment";
import { createComment, getForProduct } from "../fetch/requests/comments";

const Comments = ({ currentUserId, productId }) => {
  const [comments, setComments] = useState([]);
  const [activeComment, setActiveComment] = useState(null);
  const rootComments = comments.filter((comment) => comment.parentId === null);

  async function addComment(text, parentId) {
    console.log(text);
    var response = await createComment({
      Content: text,
      ParentId: parentId,
      ProductId: productId,
    });
    debugger;
    var data = await response.json();
    console.log(data);
    setComments([data, ...comments]);
    setActiveComment(null);
  }

  useEffect(() => {
    (async function () {
      try {
        debugger;
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
            replies={[]}
            //replies={getReplies(rootComment.id)}
            activeComment={activeComment}
            setActiveComment={setActiveComment}
            addComment={addComment}
            currentUserId={currentUserId}
          />
        ))}
      </div>
    </div>
  );
};

export default Comments;
