import React, { useState, useEffect } from 'react';
import './App.css';

function App() {
  const [posts, setPosts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    // استدعاء الـ API عبر مسار نسبي
    fetch(`/api/posts?page=1&pageSize=5`)
      .then((response) => {
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        return response.json();
      })
      .then((data) => {
        setPosts(data);
      })
      .catch((err) => {
        console.error('Error fetching posts:', err);
        setError(err.message);
      })
      .finally(() => {
        setLoading(false);
      });
  }, []);

  if (loading) {
    return <div>جاري التحميل...</div>;
  }

  if (error) {
    return <div>حدث خطأ: {error}</div>;
  }

  return (
    <div className="App">
      <h1>Taarafou Posts</h1>
      {posts.length > 0 ? (
        <ul>
          {posts.map((post) => (
            <li key={post.id}>
              <h2>{post.title}</h2>
              <p>{post.content}</p>
            </li>
          ))}
        </ul>
      ) : (
        <p>لا توجد منشورات.</p>
      )}
    </div>
  );
}

export default App;
