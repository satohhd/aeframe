<template>
  <v-menu
    bottom
    min-width="200px"
    rounded
    offset-y
  >
    <template v-slot:activator="{ on }">
      <v-btn
        icon
        x-large
        v-on="on"
      >
        <v-avatar
          :color="color"
          size="34"
          style="line-height:34px !important;"
        >
          <span class="white--text text-h6" style="font-size:1rem !important;">{{ firstChar }}</span>
        </v-avatar>
      </v-btn>
    </template>
    <v-card>
      <v-list-item-content class="justify-center">
        <div class="mx-auto text-center">
          <v-avatar
            :color="color"
            size="60"
            style="line-height:60px !important;"
          >
            <span class="white--text text-h6" style="font-size:1.7rem !important;">{{ firstChar }}</span>
          </v-avatar>
          <h3 style="margin-top:5px;">{{ userName }}</h3>
          <p class="text-caption mt-1">
            {{ email }}
          </p>
          <v-divider class="my-3"></v-divider>
          <v-btn
            outlined
            rounded
            text
            to="/auth/profile"
          >
            プロファイル
          </v-btn>
          <v-divider class="my-3"></v-divider>
          <v-btn
            outlined
            text
            @click="logout"
          >
            ログアウト
          </v-btn>
        </div>
      </v-list-item-content>
    </v-card>
  </v-menu>
</template>
<script>
import common from '~/mixin/common.js'
export default {
  mixins: [common],
  data: () => ({
  }),
  computed: {
    userName () {
      return this.$auth.user ? this.$auth.user.userName : 'guest'
    },
    firstChar () {
      return this.$auth.user && this.$auth.user.firstChar ? this.$auth.user.firstChar : this.$auth.user.userName.substr(0, 1).toUpperCase()
    },
    email () {
      return this.$auth.user ? this.$auth.user.email : ''
    },
    color () {
      return this.$auth.user && this.$auth.user.color ? this.$auth.user.color : 'blue'
    }
  },
  methods: {
    load (page) {
      return false
    },
    async logout () {
      // await this.$auth.logout(/* .... */)
      await this.$auth
        .logout({})
        .then(() => {
          // this.$auth.logout('local', {})
          // location.reload()
          // this.$router.push('/')
          // this.show = false
          setTimeout(location.reload(), 200)
          // this.$forceUpdate()
          // this.show = false
          // this.$nextTick(() => (this.show = true))
          // this.$router.push('/')
        })
        .catch(e => (this.error = e.response.data))
    }
  }
}
</script>
