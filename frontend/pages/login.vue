<template>
  <div class="app">
    <loading v-if="!show" />
    <section v-if="show" class="content">
      <v-container>
        <v-form ref="form" v-model="form" lazy-validation>
          <v-card>
            <v-alert
              type="error"
              :value="error!=null"
            >
              {{ error }}
            </v-alert>
            <v-card-title primary-title>
              <h3 class="headline mb-0">
                ログイン
              </h3>
            </v-card-title>
            <v-card-text>
              <v-text-field
                v-model="row.username"
                label="ログインID"
                required
                autofocus
              />
              <v-text-field
                v-model="row.password"
                label="パスワード"
                :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
                :type="show1 ? 'text' : 'password'"
                required
                @click:append="show1 = !show1"
                @keyup.enter="loginUser"
              />
            </v-card-text>
            <v-card-actions>
              <v-btn color="white" style="margin:20px" @click.stop.prevent="loginUser">
                ログイン
              </v-btn>
              <v-spacer />
            </v-card-actions>
            <!-- <v-btn dark style="margin:20px" @click.stop.prevent="loginWithGitHub">
              Github
            </v-btn> -->
          </v-card>
        </v-form>
      </v-container>
    </section>
  </div>
</template>

<script>
import common from '~/mixin/common.js'
export default {
  mixins: [common],
  layout: 'base',
  data () {
    return {
      show: false,
      error: null,
      show1: false,
      form: false,
      row: {
        username: '',
        password: ''
      }
    }
  },
  // computed: {
  //   loggedInUser () {
  //     return this.$auth.user == null ? '' : this.$auth.user.name ?? ''
  //     // return false
  //   }
  // },
  methods: {
    load () {
      this.show = true
      return true
    },
    async loginUser () {
      this.error = null
      const self = this
      const ret = self.validate()
      if (!ret) { return false }

      await self.$auth
        .loginWith('local', { data: self.row })
        .then((res) => {
          // console.log('ログイン')
          // self.$router.push('/')
          location.href = '/'
          // setTimeout(location.reload(), 1000)
          // self.$router.push('/')
          // self.$nextTick(() => { self.show = true })
        })
        .catch((e) => {
          console.log('login error')
          console.log(e)
          console.log('ログインエラー')
          self.error = 'IDまたはパスワードが正しくありません。'
        })
    }
  }
}
</script>
<style scoped>
.container{
  max-width:500px;
  margin-top: calc( 100vh / 2 - 200px )
}
</style>
