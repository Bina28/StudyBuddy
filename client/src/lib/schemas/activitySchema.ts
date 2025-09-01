import { z } from "zod";
import { requiredString } from "../util/util";



export const activytSchema = z.object({
  title: requiredString("Title"),
  university: requiredString("University"),
  date: z.date({ error: "Date is required" }),
  description: requiredString("Description"),
  category: requiredString("Category"),
  city: requiredString("City"),

  location: z.object({
    locationName: requiredString("Location"),
    latitude: z.coerce.number(),
    longitude: z.coerce.number(),
  }),
});

export type ActivitySchema = z.infer<typeof activytSchema>;
