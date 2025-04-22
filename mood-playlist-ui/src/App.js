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
      // Call the API to upload the selfie for mood detection
      await uploadSelfie(file);
      
      // Once the mood is detected, fetch the playlist
      const detectedMood = "Happy";  // Placeholder: Here you would get the mood from the API
      setSelectedMood(detectedMood);
      const fetchedPlaylist = await getPlaylist(userId); // Assuming userId is passed
      setPlaylist(fetchedPlaylist);  // Store the playlist in state
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

      {/* Playlist Display */}
      {playlist.length > 0 && (
        <div className="mt-6">
          <h3 className="text-lg font-semibold">Recommended Playlist</h3>
          <ul className="list-disc pl-6">
            {playlist.map((item, index) => (
              <li key={index}>
                <strong>{item.title}</strong> by {item.artist} - 
                <a className="text-blue-500" href={item.url} target="_blank" rel="noopener noreferrer">
                  Listen
                </a>
              </li>
            ))}
          </ul>
        </div>
      )}
    </div>
  );
}
