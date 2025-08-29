import { z } from "zod";

const requiredString = (fieldName: string) =>
  z
    .string({ error: `${fieldName} is required` })
    .min(1, { message: `${fieldName} is required` });

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
