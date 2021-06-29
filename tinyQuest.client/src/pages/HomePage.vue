<template>
  <div class="component">
    nani?????
    <button type="button" class="btn btn-primary" @click="createParty(1)">
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
      createParty(data) {
        partiesService.createParty(data)
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
