import { CarDocument } from "./carDocument";
import { CarImage } from "./carImage";
import { CarTopic } from "./carTopic";

export interface Car {
  id: string;
  userId: string;
  carTopicId: string;
  ownersDescription: string;
  carDocument: CarDocument;
  carImages: CarImage[];
  carTopic: CarTopic;
}

export interface CarListItemDto {
  id: string;
  userId: string;
  carTopicId: string;
  ownersDescription: string;
  carDocument: CarDocument;
  carMainImage: CarImage;
  carTopic: CarTopic;
}