export default class Shop {
    id: number;
    name: string;
    location: string;
    imagePath: string;
    sportKindId: number;

    constructor() {
      this.id = 0;
      this.name = "";
      this.location = "";
      this.imagePath = "";      
      this.sportKindId = 0;
    }
}