import { IPhoto } from "./i-photo";

export interface IUser {
  id: number;
  username: string;
  gender: string;
  age: number;
  name: string;
  created: Date;
  lastActive: Date;
  city: string;
  profilePhotoUrl: string;
  introduction ?: string;
  lookingFor ?: string;
  interests ?: string;
  photos ?: IPhoto[];
}
