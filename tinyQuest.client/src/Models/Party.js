export class Party {
  constructor(data = {}) {
    this.id = data.id || data._id
    this.creatorId = data.creatorId
    this.hero1Id = data.hero1Id
    this.hero2Id = data.hero2Id
    this.hero3Id = data.hero3Id
    this.hero4Id = data.hero4Id
  }
}
