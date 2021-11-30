<template>
  <main>
    <div class="container mx-auto pt-8">
      <div
        v-if="!$auth.loggedIn"
        class="w-full px-6 py-3 rounded-sm border text-yellow-800 bg-yellow-300 border-yellow-400"
        role="alert"
      >
        Before proceeding, please check your email for a verification link. If
        you did not receive the email,
        <button
          class="text-blue-700 cursor-pointer hover:text-blue-600 focus:outline-none focus:underline transition ease-in-out duration-150"
          @click="resendVerificationEmail"
        >
          click here to request another</button>.
      </div>
      <v-card>
        <v-list-item-content class="justify-center">
          <div class="mx-auto text-center">
            <v-avatar
              :color="loggedInUser.color"
              size="100"
              style="line-height:100px !important;"
            >
              <span class="white--text text-h6" style="font-size:2.3rem !important;">{{ loggedInUser.firstChar }}</span>
            </v-avatar>

            <h2 style="margin:50px;">ようこそ、{{ loggedInUser.userName }} さん</h2>
            <!-- <p class="text-caption mt-1">
              {{ loggedInUser.email }}
            </p> -->
            <v-divider class="my-3"></v-divider>
            <v-form ref="form" v-model="validForm" class="form-container" lazy-validation>
              <div id="name">
                <v-text-field
                  v-model="row.userName"
                  :rules="rules.userName"
                  type="text"
                  label="ユーザ名"
                  required
                  counter="25"
                  @change="setFirstChar"
                />
              </div>
              <div id="firstChar">
                <v-text-field
                  v-model="row.firstChar"
                  :rules="rules.firstChar"
                  type="text"
                  label="１文字表示"
                  counter="1"
                  required
                />
              </div>
              <div id="orgPassword">
                <v-text-field
                  v-model="row.orgPassword"
                  :rules="rules.orgPassword"
                  type="text"
                  label="変更前パスワード"
                  counter="25"
                  hint="変更したい場合のみ入力してください"
                />
              </div>
              <div id="password">
                <v-text-field
                  v-model="row.password"
                  :rules="rules.password"
                  type="text"
                  label="変更後パスワード"
                  counter="25"
                  hint="変更したい場合のみ入力してください"
                />
              </div>
              <div id="color">
                <label>カラー&emsp;<span v-if="row.color"> {{ (row.color)['hex'] }}</span>
                  <v-color-picker
                    v-model="row.color"
                    :rules="rules.color"
                    label="色"
                    dot-size="25"
                    hide-canvas
                    hide-inputs
                    hide-mode-switch
                    show-swatches
                    swatches-max-height="50"
                    width="100%"
                    required
                  />
                </label>
              </div>
            </v-form>
            <v-divider class="my-3"></v-divider>
            <v-btn
              outlined
              text
              @click="store"
            >
              保存
            </v-btn>
          </div>
        </v-list-item-content>
      </v-card>
      <!-- <div class="mt-6">loggedInUser: {{ loggedInUser }}</div> -->
    </div>
  </main>
</template>
<script>
import common from '~/mixin/common.js'
export default {
  mixins: [common],
  middleware: 'auth',
  data () {
    return {
      validForm: false,
      loggedInUser: this.$auth.user,
      row: {
        id: this.$auth.user.id,
        userName: this.$auth.user.userName,
        firstChar: this.$auth.user.firstChar,
        password: '',
        orgPassword: '',
        color: this.$auth.user.color
      },
      rules: {
        id: [],
        userName: [
          (v) => { return !!v || '必須です' },
          (v) => { return (v && v.length >= 1 && v.length <= 25) || '1文字以上、25文字以内で入力してください。' }
        ],
        normalizedUserName: [],
        email: [
          (v) => { return !!v || '必須です' },
          (v) => { return (v && v.length >= 3 && v.length <= 50) || '3文字以上、50文字以内で入力してください。' }
        ],
        normalizedEmail: [],
        emailConfirmed: [],
        passwordHash: [],
        securityStamp: [],
        concurrencyStamp: [],
        phoneNumber: [],
        phoneNumberConfirmed: [],
        twoFactorEnabled: [],
        lockoutEnd: [],
        lockoutEnabled: [],
        accessFailedCount: [],
        firstChar: [
          (v) => { return !!v || '必須です' },
          (v) => { return (v && v.length === 1) || '1文字を入力してください。' }
        ],
        orgPassword: [
          (v) => { return !v || (v.length >= 8 && v.length <= 25) || '8文字以上、25文字以内で入力してください。' }
        ],
        password: [
          // (v) => { return !!v || '必須です' },
          (v) => { return !v || !!this.row.orgPassword || '変更前のパスワードを入力してください' },
          (v) => { return !v || (v && v.length >= 8 && v.length <= 25) || '8文字以上、25文字以内で入力してください。' },
          (v) => { return (!v || /.*[A-Z]+.*/.test(v)) || '英大文字、数字、記号それぞれ１文字以上を含めてください' },
          (v) => { return (!v || /.*[0-9]+.*/.test(v)) || '英大文字、数字、記号それぞれ１文字以上を含めてください' },
          (v) => { return (!v || /.*[!-/:-@[-`{-~]+.*/.test(v)) || '大文字、数字、記号それぞれ１文字以上を含めてください' }
          // (v) => { return /^(?=.*[.?/-])(?=.*[.?/-])[a-zA-Z0-9.?/-]+.*$/.test(v) || '大文字１文字以上、数字１文字以上、記号１文字以上を含めてください' }
          // (v) => { return /^.*(?=.*[A-Z])+.*$/.test(v) || '2大文字１文字以上、数字１文字以上、記号１文字以上を含めてください' }
        ],
        color: [],
        displayOrder: []
      }
    }
  },
  methods: {
    load () {
      return false
    },
    // async resendVerificationEmail () {
    //   await this.$axios
    //     .$post('/backend/api/v1/verify-email/resend')
    //     .then(() => {
    //       this.$toast.success(
    //         'A fresh verification link has been sent to your email address.'
    //       )
    //     })
    //     .catch((e) => {
    //       this.$toast.error(e.response.data.message)
    //     })
    // },
    setFirstChar () {
      if (!this.row.userName) {
        this.row.firstChar = ''
      }
      if (!this.row.firstChar && this.row.userName) {
        this.row.firstChar = this.row.userName.slice(0, 1)
      }
    },
    store () {
      const self = this

      const ret = self.validate()
      if (!ret) { return false }

      const answer = confirm('更新しますか？')
      if (!answer) { return false }
      if (self.row.color.hex) {
        self.row.color = self.row.color.hex
      }
      self.$axios
        .put('/backend/api/profile/' + self.row.id, self.row)
        .then(function (res) {
          self.showResultMessage(res)
          location.href = '/'
        })
        .catch((error) => {
          self.errorResultCheck(error)
        })
      return false
    }
  }
}
</script>
<style scoped>
.form-container{
  display: grid;
  grid-template-columns: 1fr 1fr;
  grid-template-areas:
    "name firstChar"
    "orgPassword color"
    "password color";
}
 #color{
  grid-area: color;
  margin:0 100px 0 10px;
 }
 #name{
  grid-area: name;
  margin:0 100px 0 100px;
 }
 #firstChar{
  grid-area: firstChar;
  margin:0 100px 0 10px;
 }

 #order{
  grid-area: order;
  margin:0 100px 0 10px;
 }

 #password{
  grid-area: password;
  margin:0 100px 0 100px;
 }

 #orgPassword{
  grid-area: orgPassword;
  margin:0 100px 0 100px;
 }

</style>
