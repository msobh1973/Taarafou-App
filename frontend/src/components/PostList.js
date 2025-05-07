// File: src/components/PostList.js
import React from 'react';
import { FaTrash, FaEdit } from 'react-icons/fa';

export default function PostList({ posts, apiUrl, onEdit, onDelete }) {
  const handleDelete = async id => {
    if (!window.confirm('هل أنت متأكد من الحذف؟')) return;
    await fetch(`${apiUrl}/api/posts/${id}`, { method: 'DELETE' });
    onDelete();
  };

  return (
    <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
      {posts.map(post => (
        <div key={post.id} className="bg-white rounded-xl shadow-lg p-6 flex flex-col">
          <h2 className="text-2xl font-semibold mb-2">{post.title}</h2>
          <p className="text-gray-700 mb-4 flex-1">{post.content}</p>
          <div className="text-sm text-gray-500 mb-4">
            <div>🆕 {new Date(post.createdAt).toLocaleString()}</div>
            <div>✏️ {new Date(post.updatedAt).toLocaleString()}</div>
          </div>
          <div className="flex justify-end space-x-2">
            <button
              onClick={() => onEdit(post)}
              className="flex items-center px-3 py-1 bg-yellow-300 rounded hover:bg-yellow-400"
            >
              <FaEdit className="mr-1" /> تعديل
            </button>
            <button
              onClick={() => handleDelete(post.id)}
              className="flex items-center px-3 py-1 bg-red-400 text-white rounded hover:bg-red-500"
            >
              <FaTrash className="mr-1" /> حذف
            </button>
          </div>
        </div>
      ))}
    </div>
  );
}
