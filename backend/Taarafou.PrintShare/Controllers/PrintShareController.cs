import React from 'react';
import { useMutation } from '@apollo/client';
import { SHARE_POST_MUTATION } from '../graphql/mutations';

function SharePostButton({ postId }) {
  const [sharePost] = useMutation(SHARE_POST_MUTATION);

  const handleShare = () => {  
    sharePost({ variables: { postId } });
    // Open native share UI
    navigator.share({
      title: 'Check out this post on Taarafou',
      text: 'I found this interesting post on Taarafou and wanted to share it with you.',
      url: `https://taarafou.com/post/${postId}`,
    });
  };

  return (
    <button onClick={handleShare}>
      Share Post
    </button>  
  );
}

function PrintPostButton({ postId }) {
  const handlePrint = () => {
    window.open(`/post/${postId}/print`, '_blank');
  };

  return (  
    <button onClick={handlePrint}>
      Print Post
    </button>
  );
}