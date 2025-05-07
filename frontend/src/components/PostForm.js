// File: src/components/PostForm.js
import React, { useState, useEffect } from 'react';

export default function PostForm({ apiUrl, editingPost, onCancel, onSuccess }) {
  const [title, setTitle] = useState('');
  const [body, setBody]   = useState('');
  const [loading, setLoading] = useState(false);

  // عند الدخول في وضع التحرير، عيّن الحقول
  useEffect(() => {
    if (editingPost) {
      setTitle(editingPost.title);
      setBody(editingPost.content);
    } else {
      setTitle('');
      setBody('');
    }
  }, [editingPost]);

  const handleSubmit = async e => {
    e.preventDefault();
    if (!title.trim() || !body.trim()) return;

    setLoading(true);
    try {
      const method = editingPost ? 'PUT' : 'POST';
      const url = editingPost
        ? `${apiUrl}/api/posts/${editingPost.id}`
        : `${apiUrl}/api/posts`;

      const res = await fetch(url, {
        method,
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ title: title.trim(), body: body.trim() }),
      });

      if (!res.ok) throw new Error(`HTTP ${res.status}`);
      // بعد النجاح
      onSuccess();
    } catch (err) {
      console.error('Form submit error:', err);
      alert('خطأ في حفظ المنشور: ' + err.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="flex flex-col md:flex-row md:space-x-4 mb-6">
      <input
        type="text"
        placeholder="العنوان"
        value={title}
        onChange={e => setTitle(e.target.value)}
        required
        className="border rounded p-2 mb-2 md:mb-0 flex-1"
        disabled={loading}
      />
      <textarea
        placeholder="المحتوى"
        value={body}
        onChange={e => setBody(e.target.value)}
        required
        className="border rounded p-2 mb-2 md:mb-0 flex-1"
        disabled={loading}
      />
      <div className="flex space-x-2">
        <button
          type="submit"
          className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 disabled:opacity-50"
          disabled={loading}
        >
          {editingPost ? 'تحديث منشور' : 'إنشاء منشور'}
        </button>
        {editingPost && (
          <button
            type="button"
            onClick={onCancel}
            className="px-4 py-2 bg-gray-300 rounded hover:bg-gray-400"
            disabled={loading}
          >
            إلغاء
          </button>
        )}
      </div>
    </form>
  );
}
