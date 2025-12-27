import { useEffect, useState, FormEvent } from "react";
import { PostInfo } from "../../types/post";
import api from "../../services/api";
import { AxiosResponse } from "axios";
import { useTranslation } from "react-i18next";

export default function PostsList() {
  const { t } = useTranslation();
  const [posts, setPosts] = useState<PostInfo[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const [newTitle, setNewTitle] = useState("");
  const [newContent, setNewContent] = useState("");
  const [submitting, setSubmitting] = useState(false);

  const [expandedComments, setExpandedComments] = useState<
    Record<string, boolean>
  >({});
  const [commentInputs, setCommentInputs] = useState<Record<string, string>>(
    {}
  );

  useEffect(() => {
    fetchPosts();
  }, []);

  const fetchPosts = async () => {
    try {
      setLoading(true);
      const response = (await api.Posts.getPosts()) as AxiosResponse;
      setPosts(response.data);
    } catch (err) {
      console.error(err);
      setError("Failed to fetch posts.");
    } finally {
      setLoading(false);
    }
  };

  const handleCreatePost = async (e: FormEvent) => {
    e.preventDefault();
    if (!newTitle.trim() || !newContent.trim()) return;

    try {
      setSubmitting(true);
      await api.Posts.createPost({ title: newTitle, content: newContent });
      setNewTitle("");
      setNewContent("");
      await fetchPosts();
    } catch (err) {
      console.error(err);
      setError("Failed to create post.");
    } finally {
      setSubmitting(false);
    }
  };

  const handleReaction = async (postId: string) => {
    try {
      await api.Posts.reactPost(postId, { postId: postId }); // Assuming this API toggles reaction
      await fetchPosts();
    } catch {
      console.error("Failed to react to post.");
    }
  };

  const handleAddComment = async (postId: string) => {
    const content = commentInputs[postId]?.trim();
    if (!content) return;

    try {
      await api.Comments.createComment(postId, {
        postId: postId,
        content: content,
      });
      setCommentInputs((prev) => ({ ...prev, [postId]: "" }));
      await fetchPosts();
    } catch {
      console.error("Failed to add comment.");
    }
  };

  return (
    <div className="max-w-4xl mx-auto p-4 space-y-6">
      {/* Post creation form */}
      <form
        onSubmit={handleCreatePost}
        className="bg-white shadow rounded-lg p-4 border border-gray-200 space-y-3"
      >
        <h2 className="text-xl font-semibold text-gray-800">
          {t("Create New Post")}
        </h2>
        <input
          type="text"
          placeholder={t("Title")}
          value={newTitle}
          onChange={(e) => setNewTitle(e.target.value)}
          className="w-full px-3 py-2 border rounded-md"
        />
        <textarea
          placeholder={t("Content")}
          value={newContent}
          onChange={(e) => setNewContent(e.target.value)}
          rows={4}
          className="w-full px-3 py-2 border rounded-md"
        />
        <button
          type="submit"
          disabled={submitting}
          className="bg-gray-600 text-white px-4 py-2 rounded hover:bg-gray-700 disabled:opacity-50"
        >
          {submitting ? t("Posting...") : t("Post")}
        </button>
      </form>

      {loading && <div className="text-center py-4">Loading...</div>}
      {error && <div className="text-red-500 text-center py-4">{error}</div>}
      {!loading && !error && posts.length === 0 && (
        <div className="text-center text-gray-500">{t("No posts found.")}</div>
      )}

      {/* Posts List */}
      <div className="space-y-4">
        {posts.map((post) => {
          const showAll = expandedComments[post.id];
          const visibleComments = showAll
            ? post.comments || []
            : (post.comments || []).slice(0, 3);
          const userAvatar = post.user?.avatarUrl;
          const userInitial = post.user?.userNickname?.charAt(0) || "?";

          return (
            <div
              key={post.id}
              className="bg-white shadow rounded-lg p-4 border border-gray-200 space-y-3"
            >
              <div className="flex items-center gap-3">
                {userAvatar ? (
                  <img
                    src={userAvatar}
                    alt="avatar"
                    className="w-10 h-10 rounded-full object-cover"
                  />
                ) : (
                  <div className="w-10 h-10 rounded-full bg-gray-300 flex items-center justify-center text-white font-bold">
                    {userInitial}
                  </div>
                )}
                <div>
                  <div className="font-semibold">
                    {post.user?.userNickname || "Unknown"}
                  </div>
                  <div className="text-sm text-gray-500">
                    {new Date(post.createdAt).toLocaleString("eu-EU", {
                      timeZone: "Europe/Kiev", // or 'Europe/Minsk', etc. (GMT+3)
                      year: "numeric",
                      month: "2-digit",
                      day: "2-digit",
                      hour: "2-digit",
                      minute: "2-digit",
                      second: "2-digit",
                    })}
                  </div>
                </div>
              </div>

              <h3 className="text-lg font-bold">{post.title}</h3>
              <p className="text-gray-700">{post.content}</p>

              {/* Reactions and Comments Summary */}
              <div className="flex items-center justify-between text-sm text-gray-500 mt-2">
                <button
                  onClick={() => handleReaction(post.id)}
                  className="flex items-center gap-1 hover:text-blue-600 hover:bg-gray-400 bg-gray-100 px-2 py-1 rounded-md transition-colors"
                >
                  <span className="material-icons">❤</span>
                  {post.reactionsCount}
                </button>
                <span>{post.comments?.length ?? 0} 💬</span>
              </div>

              {/* Comments List */}
              <div className="space-y-2 mt-2">
                {visibleComments.map((comment, index) => (
                  <div
                    key={comment.id || index}
                    className="flex items-start gap-2"
                  >
                    <img
                      src={comment.user.avatarUrl}
                      alt="avatar"
                      className="w-10 h-10 rounded-full object-cover"
                    />
                    <div className="text-sm bg-gray-100 p-2 rounded-md">
                      <strong>{comment.user?.userNickname || "Anon"}:</strong>{" "}
                      {comment.content}
                    </div>
                  </div>
                ))}
                {(post.comments?.length ?? 0) > 3 && (
                  <button
                    onClick={() =>
                      setExpandedComments((prev) => ({
                        ...prev,
                        [post.id]: !prev[post.id],
                      }))
                    }
                    className="text-gray-600 text-sm hover:underline"
                  >
                    {showAll ? t("Hide comments") : t("Show all comments")}
                  </button>
                )}
              </div>

              {/* Add Comment */}
              <div className="flex mt-2 gap-2">
                <input
                  type="text"
                  placeholder={t("Write a comment...")}
                  value={commentInputs[post.id] || ""}
                  onChange={(e) =>
                    setCommentInputs((prev) => ({
                      ...prev,
                      [post.id]: e.target.value,
                    }))
                  }
                  className="flex-1 px-2 py-1 border rounded-md"
                />
                <button
                  onClick={() => handleAddComment(post.id)}
                  className="bg-gray-600 text-white px-3 py-1 rounded hover:bg-gray-700"
                >
                  {t("Send")}
                </button>
              </div>
            </div>
          );
        })}
      </div>
    </div>
  );
}
