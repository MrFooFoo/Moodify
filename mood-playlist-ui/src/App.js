import React, { useState } from "react";
import { submitMood, uploadSelfie, getPlaylist } from "./api";  // Assuming these API methods are defined in your `api.js`
import "./index.css";

const moods = [
  { mood: "Happy", emoji: "😊" },
  { mood: "Sad", emoji: "😢" },
  { mood: "Chill", emoji: "😌" },
  { mood: "Energetic", emoji: "⚡" },
];

export default function App() {
  const [selectedMood, setSelectedMood] = useState(null);
  const [selfie, setSelfie] = useState(null);
  const [playlist, setPlaylist] = useState([]);  // To store the playlist
  const [userId, setUserId] = useState("abc");  // Example userId, you can update this based on your logic

  const handleMoodSelect = async (mood) => {
    setSelectedMood(mood);
    try {
      // Call the API to submit the mood (this could trigger the Kafka producer)
      await submitMood(userId, mood);
      
      // Call the API to fetch the playlist based on the mood
      const fetchedPlaylist = await getPlaylist(userId); // Assuming userId is passed
      setPlaylist(fetchedPlaylist);  // Store the playlist in state
    } catch (error) {
      console.error("Error submitting mood:", error);
    }
  };

  const handleSelfieUpload = async (event) => {
    const file = event.target.files[0];
    setSelfie(file);
    try {
      const result = await uploadSelfie(file, userId); // pass userId
      const detectedMood = result.mood; // extract mood from response
  
      setSelectedMood(detectedMood);
      const fetchedPlaylist = await getPlaylist(userId);
      setPlaylist(fetchedPlaylist);
    } catch (error) {
      console.error("Error uploading selfie:", error);
    }
  };
  

  return (
    <div className="min-h-screen bg-gray-100 flex flex-col items-center justify-center gap-6 p-4">
      <h1 className="text-4xl font-bold text-gray-800 mb-6">🎶 Moodify</h1>

      {/* Mood Selector */}
      <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
        {moods.map(({ mood, emoji }) => (
          <button
            key={mood}
            className={`p-4 bg-white rounded-2xl shadow-md text-xl hover:scale-105 transition-all ${
              selectedMood === mood ? "border-4 border-indigo-500" : ""
            }`}
            onClick={() => handleMoodSelect(mood)}
          >
            <span role="img" aria-label={mood} className="text-3xl block mb-2">
              {emoji}
            </span>
            {mood}
          </button>
        ))}
      </div>

      {/* OR separator */}
      <div className="my-4 text-gray-500">— OR —</div>

      {/* Selfie Upload */}
      <label className="cursor-pointer bg-white p-4 rounded-xl shadow-md hover:bg-indigo-50 transition-all">
        📸 Upload a selfie
        <input
          type="file"
          accept="image/*"
          onChange={handleSelfieUpload}
          className="hidden"
        />
      </label>

      {selfie && (
        <p className="text-sm text-gray-600 mt-2">
          Selected file: <strong>{selfie.name}</strong>
        </p>
      )}

{playlist.length > 0 && (
  <div className="mt-6 w-full max-w-5xl">
    <h3 className="text-2xl font-semibold mb-4 text-center text-gray-800">🎧 Recommended Playlist</h3>
    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6">
      {playlist.map((item, index) => {
        let videoId = "";
        try {
          const url = new URL(item.url);
          if (url.hostname.includes("youtu.be")) {
            videoId = url.pathname.slice(1);
          } else {
            videoId = url.searchParams.get("v") || "";
          }
        } catch (e) {
          videoId = "";
        }

        const thumbnailUrl = videoId
          ? `https://img.youtube.com/vi/${videoId}/hqdefault.jpg`
          : "https://via.placeholder.com/320x180?text=No+Thumbnail";

        return (
          <div key={index} className="bg-white rounded-2xl shadow-lg overflow-hidden hover:shadow-2xl transition-all">
            <img src={thumbnailUrl} alt={item.title} className="w-full h-48 object-cover" />
            <div className="p-4">
              <h4 className="font-bold text-lg text-gray-800">{item.title}</h4>
              <p className="text-gray-600 text-sm mb-2">by {item.artist}</p>
              <a
                href={item.url}
                target="_blank"
                rel="noopener noreferrer"
                className="text-indigo-600 text-sm font-medium hover:underline"
              >
                ▶️ Watch on YouTube
              </a>
            </div>
          </div>
        );
      })}
    </div>
  </div>
)}

    </div>
  );
}
