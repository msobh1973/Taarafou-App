// File: C:\Users\U\Documents\Taarafou-App\frontend\src\App.js

import React, { useState, useEffect } from 'react';
import { ToastContainer, toast }    from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import PostForm                     from './components/PostForm';
import PostList                     from './components/PostList';

const API_URL = 'http://localhost:5160';

function App() {
  const [posts, setPosts]             = useState([]);
  const [loading, setLoading]         = useState(true);
  const [searchQuery, setSearchQuery] = useState('');
  const [page, setPage]               = useState(1);
  const [pageSize]                    = useState(5);
  const [totalPages, setTotalPages]   = useState(1);

  // لحفظ المنشور الجاري تحريره (null = وضع الإنشاء)
  const [editingPost, setEditingPost] = useState(null);

  // جلب صفحة من المنشورات مع فلترة البحث server-side
  const fetchPosts = async () => {
    try {
      setLoading(true);
      const url = new URL(`${API_URL}/api/posts`);
      url.searchParams.append('page', page);
      url.searchParams.append('pageSize', pageSize);
      if (searchQuery.trim()) {
        url.searchParams.append('search', searchQuery.trim());
      }

      const res = await fetch(url);
      if (!res.ok) throw new Error(`HTTP ${res.status}`);
      const { items, totalPages: tp } = await res.json();

      // توحيد أسماء الحقول
      const normalized = items.map(p => ({
        id: p.id,
        title: p.title,
        content: p.body,
        createdAt: p.createdAt  ?? p.CreatedAt,
        updatedAt: p.updatedAt  ?? p.UpdatedAt,
      }));

      setPosts(normalized);
      setTotalPages(tp);
    } catch (err) {
      console.error('Fetch error:', err);
      toast.error('خطأ في جلب المنشورات: ' + err.message);
    } finally {
      setLoading(false);
    }
  };

  // إعادة الجلب عند تغيّر الصفحة أو نص البحث
  useEffect(() => {
    fetchPosts();
  }, [page, searchQuery]);

  // بعد نجاح الإنشاء أو التعديل، نخرُج من وضع التحرير ونعيد الجلب
  const handleFormSuccess = () => {
    setEditingPost(null);
    // لو تريد البقاء في الصفحة الأولى بعد تحرير يمكنك uncomment:
    // setPage(1);
    fetchPosts();
  };

  if (loading) {
    return <div className="p-4">جارٍ تحميل المنشورات…</div>;
  }

  return (
    <div className="container mx-auto p-4">
      {/* إشعارات Toast */}
      <ToastContainer position="top-right" autoClose={3000} />

      <h1 className="text-3xl font-bold mb-4">Taarafou Posts</h1>

      {/* حقل البحث */}
      <input
        type="text"
        placeholder="ابحث عن عنوان..."
        value={searchQuery}
        onChange={e => { setSearchQuery(e.target.value); setPage(1); }}
        className="border rounded p-2 mb-4 w-full"
      />

      {/* نموذج الإنشاء/التحرير */}
      <PostForm
        apiUrl={API_URL}
        editingPost={editingPost}
        onCancel={() => setEditingPost(null)}
        onSuccess={handleFormSuccess}
      />

      {/* قائمة المنشورات أو رسالة عدم وجودها */}
      {posts.length === 0 ? (
        <p>لا توجد منشورات.</p>
      ) : (
        <PostList
          posts={posts}
          apiUrl={API_URL}
          onEdit={setEditingPost}
          onDelete={fetchPosts}
        />
      )}

      {/* عنصر تحكّم Pagination */}
      <div className="flex justify-center items-center mt-6 space-x-4">
        <button
          onClick={() => setPage(p => Math.max(1, p - 1))}
          disabled={page === 1}
          className="px-3 py-1 bg-gray-300 rounded disabled:opacity-50"
        >
          السابق
        </button>
        <span>صفحة {page} من {totalPages}</span>
        <button
          onClick={() => setPage(p => Math.min(totalPages, p + 1))}
          disabled={page === totalPages}
          className="px-3 py-1 bg-gray-300 rounded disabled:opacity-50"
        >
          التالي
        </button>
      </div>
    </div>
  );
}

export default App;
