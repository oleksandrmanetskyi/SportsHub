export default class TrainingProgram {
    id: number;
    name: string;
    description: string;
    imagePath: string;
    sportKindId: number;
    nutritionId: number | null;
  
    constructor() {
      this.id = 0;
      this.name = "";
      this.description = "";
      this.imagePath = "";
      this.sportKindId = 0;
      this.nutritionId = 0;
    }
}