import axios from "axios";

const MOOD_DETECTION_URL = "http://localhost:5011/api/Mood/detect";  // URL for mood detection
const PLAYLIST_URL = "http://localhost:5008/api/Playlist";  // URL for playlist

export const submitMood = async (userId, mood) => {
  try {
    const response = await axios.post(`${MOOD_DETECTION_URL}?userId=${userId}&mood=${mood}`);
    return response.data;
  } catch (error) {
    console.error("Error submitting mood:", error);
    throw error;
  }
};


export const uploadSelfie = async (file, userId) => {
  const formData = new FormData();
  formData.append("selfie", file);
  formData.append("userId", userId); // include userId

  try {
    const response = await axios.post("http://localhost:5011/api/Mood/uploadSelfie", formData, {
      headers: { "Content-Type": "multipart/form-data" },
    });
    return response.data;
  } catch (error) {
    console.error("Error uploading selfie:", error);
    throw error;
  }
};


export const getPlaylist = async (userId) => {
  try {
    const response = await axios.get(`${PLAYLIST_URL}/${userId}`);
    return response.data;
  } catch (error) {
    console.error("Error fetching playlist:", error);
    throw error;
  }
};
