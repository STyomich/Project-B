import { ChangeEvent } from "react";
import { useTranslation } from "react-i18next";

export default function LanguageSelector() {
  const { i18n } = useTranslation();
  const currentLanguage = i18n.language;

  const handleChange = (event: ChangeEvent<HTMLSelectElement>) => {
    const newLanguage = event.target.value;
    i18n.changeLanguage(newLanguage);
  };

  return (
    <select className="text-white" value={currentLanguage} onChange={handleChange}>
      <option className="text-black" value="en">English</option>
      <option className="text-black" value="ua">Українська</option>
    </select>
  );
}
