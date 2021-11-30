<template>
  <div id="sample-page">
    <v-container fluid>
      <v-card elevation="2">
        <v-card-title>
          <p>SAMPLE</p>
        </v-card-title>
        <v-card-text>
          <v-form v-model="valid">
            <v-text-field
              v-model="message1"
              label="Item1"
              :rules="rules.code1"
              maxlength="5"
              counter="5"
              outlined
              dense
              required
            />
            <v-text-field
              v-model="message2"
              label="Item2"
              :rules="rules.code2"
              maxlength="10"
              counter="10"
              outlined
              dense
              required
            />
            <v-text-field
              v-model="message3"
              label="Item3"
              :rules="rules.code3"
              counter="10"
              outlined
              dense
              required
            />
            <v-text-field
              v-model="message4"
              label="Item4"
              :rules="rules.code4"
              outlined
              dense
              required
            />
          </v-form>
        </v-card-text>
      </v-card>
    </v-container>
  </div>
</template>
<script>
export default {
  data() {
    return {
      valid: false,
      message1: 'Hey!',
      message2: 'Hey!',
      message3: 'Hey!',
      message4: 'Hey!',
      message: '',
      rules: {
        code1: [
          v => !!v || '必須項目です。',
          v => v.length <= 5 || 'Its must be less than 10 characters'
        ],
        code2: [
          v => !!v || '必須項目です。',
          v => v.length <= 10 || 'Its must be less than 10 characters'
        ],
        code3: [
          v => !!v || '必須項目です。',
          v => v.length <= 10 || 'Its must be less than 10 characters'
        ],
        code4: [
          v => !!v || '必須項目です。',
          v => v.length <= 100 || 'Its must be less than 100 characters',
          v => { return !v || /^(https?|ftp):\/\/([a-zA-Z0-9.-]+(:[a-zA-Z0-9.&%$-]+)*@)*((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])){3}|([a-zA-Z0-9-]+\.)*[a-zA-Z0-9-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{2}))(:[0-9]+)*(\/($|[a-zA-Z0-9.,?'\\+&%$#=~_-]+))*$/.test(v) || '正しいURLを入力してください。' }
        ],
        name: [
          v => !!v || 'required',
          v => v.length <= 20 || 'Its must be less than 10 characters'
        ]
      }
    }
  }
}
</script>
<style scoped>

</style>
<style lang="scss">
/* .v-text-field--outlined, .v-text-field--solo{
     border-radius: 0;
 } */
 #sample-page{
  .v-input {
    padding-left: 100px !important;
  }
  .v-label {
    left: -100px !important;
  }
  .v-input legend {
    width:0 !important;
    /* display: none !important; */
    /* visibility: hidden !important; */
  }
  .v-label--active {
    transform: translateY(0) scale(1) !important;
  }
}
</style>
