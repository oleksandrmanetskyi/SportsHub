export default class News {
    id: number;
    title: string;
    description: string;
    imagePath: string;
    sportKindId: number;

    constructor() {
      this.id = 0;
      this.title = "";
      this.description = "";
      this.imagePath = "";
      this.sportKindId = 0;
    }
}