import React, { useRef, useState } from "react";
import api from "../../services/api";
import { useParams } from "react-router-dom";

interface ImagePreview {
  file: File;
  url: string;
  isMain: boolean;
  uploaded: boolean;
}

export default function UploadCarImages() {
  const [images, setImages] = useState<ImagePreview[]>([]);
  const inputRef = useRef<HTMLInputElement | null>(null);
  const carId = useParams().carId as string;

  const handleImageChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const files = e.target.files;
    if (!files) return;

    const selectedFiles = Array.from(files);
    const newPreviews: ImagePreview[] = selectedFiles.map((file) => ({
      file,
      url: URL.createObjectURL(file),
      isMain: false,
      uploaded: false,
    }));

    setImages((prev) => [...prev, ...newPreviews]);

    // Clear input
    if (inputRef.current) inputRef.current.value = "";
  };

  const uploadImages = async () => {
    for (let i = 0; i < images.length; i++) {
      if (images[i].uploaded) continue;

      const formData = new FormData();
      formData.append("file", images[i].file);

      try {
        const response = await api.CarImage.uploadCarImage(
          formData,
          carId,
          images[i].isMain
        );
        if (response.status !== 200) {
          alert("Error uploading image");
        }

        setImages((prev) =>
          prev.map((img, index) =>
            index === i ? { ...img, uploaded: true } : img
          )
        );
      } catch (err) {
        console.error(`Upload failed for image ${images[i].file.name}:`, err);
        alert(`Upload failed for ${images[i].file.name}`);
      }
    }
  };

  const toggleMainImage = (index: number) => {
    setImages((prev) => {
      return prev.map((img, i) => ({
        ...img,
        isMain: i === index ? !img.isMain : false,
      }));
    });
  };

  return (
    <div className="flex flex-col items-center p-6 bg-gray-100 min-h-screen fade-in">
      <input
        ref={inputRef}
        id="fileInput"
        type="file"
        multiple
        accept="image/*"
        onChange={handleImageChange}
        className="hidden"
      />

      <label
        htmlFor="fileInput"
        className="px-4 py-2 font-semibold rounded text-white bg-gray-700 hover:bg-gray-800 cursor-pointer"
      >
        Upload Images
      </label>

      <div className="flex gap-4 mt-4 flex-wrap">
        {images.map((img, index) => (
          <div key={index} className="relative flex flex-col items-center">
            <img
              src={img.url}
              alt={`preview-${index}`}
              className="w-[150px] h-[100px] object-cover border cursor-pointer"
              style={{
                border: img.isMain ? "3px solid green" : "1px solid gray",
              }}
              onClick={() => toggleMainImage(index)}
            />
            {img.isMain && (
              <div className="absolute top-1 left-1 bg-green-600 text-white px-2 py-0.5 text-xs rounded">
                Main
              </div>
            )}
            {img.uploaded && (
              <div className="text-green-600 mt-1 text-sm">✅ Done</div>
            )}
          </div>
        ))}
      </div>

      {images.length > 0 && (
        <button
          className="px-6 mt-6 py-2 rounded transition bg-green-600 text-white hover:bg-green-700"
          onClick={uploadImages}
        >
          🚀 Submit Images
        </button>
      )}
    </div>
  );
}
