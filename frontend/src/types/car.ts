import { CarDocument } from "./carDocument";
import { CarImage } from "./carImage";
import { CarTopic } from "./carTopic";
import { RegistrationPlate } from "./registrationPlate";

export interface Car {
  id: string;
  userId: string;
  carTopicId: string;
  ownersDescription: string;
  carDocument: CarDocument;
  registrationPlate: RegistrationPlate;
  carImages: CarImage[];
  carTopic: CarTopic;
}

export interface CarListItemDto {
  id: string;
  userId: string;
  carTopicId: string;
  ownersDescription: string;
  carDocument: CarDocument;
  registrationPlate: RegistrationPlate;
  carMainImage: CarImage;
  carTopic: CarTopic;
}

export interface CarFormData {
  carTopic: CarTopic;
  registrationCountry: string;
  registrationText: string;
  ownersDescription: string;
}