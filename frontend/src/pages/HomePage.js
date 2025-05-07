import React from 'react';
import PostFeed from '../components/PostFeed';
import CultureLibrary from '../components/CultureLibrary';
import TranslateForm from '../components/TranslateForm';
import SpeechToTextForm from '../components/SpeechToTextForm';
import TextToSpeechForm from '../components/TextToSpeechForm';

function HomePage() {
  return (
    <div>
      <header>
        <h1>Welcome to Taarafou</h1>
        <p>The AI-powered social platform for connecting across cultures.</p>
      </header>
      <main>
        <PostFeed />
        <aside>
          <CultureLibrary />
          <TranslateForm />
          <SpeechToTextForm />  
          <TextToSpeechForm />
        </aside>
      </main>
    </div>
  );
}

export default HomePage;