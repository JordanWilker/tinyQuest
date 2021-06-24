import { AppState } from '../AppState'
// import router from '../router'
// import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { Party } from '../models/Party'
import { logger } from '../utils/Logger'
import { Profile } from '../models/Profile'
// import { Hero } from '../models/Hero'

class PartiesService {
  async getParties() {
    const res = await api.get('api/parties')
    AppState.parties = res.data.map(d => new Party(d))
  }

  async getPartyById(id) {
    const res = await api.get(`api/parties/${id}/yeah`)
    AppState.activeParty = new Party(res.data)
    logger.log(res.status)
  }

  async createParty() {
    try {
      const party = await api.post('api/heroes')
      // await api.post('api/heroes')
      // await api.post('api/heroes')
      // await api.put('api/parties/' + party.id, { hero1Id: `${hero1Info.id}`, hero2Id: `${hero2Info.id}` })
      // this.getParties()
      logger.log(party.data)
    } catch (error) {
      logger.error(error)
    }
  }

  async deleteParty(id) {
    try {
      await api.delete('api/parties/' + `${id}`)
      this.getPartiesByAccount()
      this.getParties()
    } catch (error) {
      logger.error(error)
    }
  }

  async getPartiesByProfileId(id) {
    try {
      const res = await api.get('api/profiles/' + id + '/parties')
      AppState.parties = res.data.map(d => new Party(d))
      logger.log(res.data)
    } catch (error) {

    }
  }

  async getProfileById(id) {
    const res = await api.get('api/profiles/' + id)
    AppState.activeProfile = new Profile(res.data)
  }

  async getPartiesByAccount() {
    const res = await api.get('account/parties')
    AppState.userParties = res.data.map(u => new Party(u))
  }
}
export const partiesService = new PartiesService()
