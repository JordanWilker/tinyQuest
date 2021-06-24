export class Profile {
  constructor(data = {}) {
    this.id = data.id || data._id || 'placeholder id'
    this.creatorId = data.creatorId || 'test creatorId'
    this.name = data.name || 'b'
    this.email = data.email
    this.picture = data.picture
  }
}
