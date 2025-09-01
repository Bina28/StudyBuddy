type Activity = {
  id: string;
  title: string;
  date: string;
  description: string;
  category: string;
  isCancelled: boolean;
  city: string;
  university: string;
  locationName: string;
  latitude: number;
  longitude: number;
};

type User = {
  id: string;
  email: string;
  displayName: string;
  imageUrl?: string;
};

type LocationIQSuggestion = {
  place_id: string;
  licence: string;
  osm_type: string;
  osm_id: string;
  lat: string;
  lon: string;
  display_name: string;
  address: LocationIQAddress;
  boundingbox: string[];
};

type LocationIQAddress = {
  attraction: string;
  house_number: string;
  road: string;
  quarter: string;
  suburb: string;
  city?: string;
  town?: string;
  village?: string;
  state_district: string;
  state: string;
  postcode: string;
  country: string;
  country_code: string;
};
