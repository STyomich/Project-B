import { useRef, useState } from "react";
import { useParams } from "react-router-dom";
import api from "../../services/api";
import { useTranslation } from "react-i18next";

export default function UploadCarDocuments() {
  const { t } = useTranslation();
  const carId = useParams().carId as string;
  const [selectedFile, setSelectedFile] = useState<File | null>(null);
  const [previewUrl, setPreviewUrl] = useState<string | null>(null);

  const fileInputRef = useRef<HTMLInputElement>(null);

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (file && file.type === "application/pdf") {
      setSelectedFile(file);
      setPreviewUrl(URL.createObjectURL(file));
    } else {
      alert("Please select a valid PDF file.");
      setSelectedFile(null);
      setPreviewUrl(null);
    }
  };

  const handleSubmit = async () => {
    if (!selectedFile) return;

    const formData = new FormData();
    formData.append("file", selectedFile);
    formData.append("carId", carId);

    try {
      const response = await api.CarDocuments.uploadCarDocuments(
        formData,
        carId
      );
      if (response.status === 200) {
        alert(t("File uploaded successfully!"));
      } else {
        alert(t("Failed to upload file."));
      }
    } catch (error) {
      console.error("Upload error:", error);
      alert(t("An error occurred while uploading."));
    }
  };

  return (
    <div className="flex flex-col gap-4 items-center p-6">
      <h2 className="text-2xl font-semibold">{t("Upload Car Document")} (PDF)</h2>

      {/* Hidden file input */}
      <input
        type="file"
        accept="application/pdf"
        ref={fileInputRef}
        onChange={handleFileChange}
        className="hidden"
      />

      {/* Trigger Button */}
      <button
        onClick={() => fileInputRef.current?.click()}
        className="px-4 py-2 font-semibold rounded text-white bg-gray-700 hover:bg-gray-800 cursor-pointer"
      >
        {selectedFile ? t("Change File") : t("Choose PDF File")}
      </button>

      {/* Preview */}
      {previewUrl && (
        <iframe
          src={previewUrl}
          title="PDF Preview"
          className="border rounded w-full max-w-3xl h-[500px]"
        />
      )}

      {/* Submit Button */}
      <button
        onClick={handleSubmit}
        disabled={!selectedFile}
        className={`px-6 py-2 rounded transition ${
          selectedFile
            ? "bg-green-600 text-white hover:bg-green-700"
            : "bg-gray-400 text-gray-700 cursor-not-allowed"
        }`}
      >
        {t("Submit")}
      </button>
    </div>
  );
}
