<template>
  <div class="component">
    nani?????
    <button type="button" class="btn btn-primary" @click="createParty()">
      Create Party
    </button>
  </div>
</template>

<script>
import { partiesService } from '../services/PartiesService'
export default {
  name: 'Component',
  setup() {
    return {
      createParty() {
        partiesService.createParty()
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
