export class Location
{
    lat: number;
    lng: number;
    constructor() {
    this.lat = 0;
    this.lng = 0;
    }
}
export class Geometry
{
    location: Location
    constructor() {
    this.location = new Location();
    }
}
export default class Place 
{
    place_id: string;
    name: string;
    business_status: string;
    formatted_address: string;
    geometry: Geometry;
    icon: string;
    icon_background_color: string;
    icon_mask_base_uri: string;
    rating: number;
  
    constructor() {
      this.place_id = "";
      this.name = "";
      this.business_status = "";
      this.formatted_address = "";
      this.geometry = new Geometry();
      this.icon = "";
      this.icon_background_color = "";
      this.icon_mask_base_uri = "";
      this.rating = 0;
    }
}