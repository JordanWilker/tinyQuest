export class Hero {
  constructor(data = {}) {
    this.id = data.id || data._id
    this.creatorId = data.creatorId
    this.raceId = data.raceId
    this.careerId = data.careerId
    this.health = data.health
    this.rangePower = data.rangePower
    this.magicPower = data.magicPower
    this.swordPower = data.swordPower
  }
}
