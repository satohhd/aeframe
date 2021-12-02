<template>
  <v-app dark>
    <v-navigation-drawer
      v-model="drawer"
      :mini-variant="miniVariant"
      :clipped="clipped"
      fixed
      app
    >
      <!-- <v-list>
        <v-list-item
          v-for="(item, i) in items"
          :key="i"
          :to="item.to"
          
          router
          exact
        >
          <v-list-item-action>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title v-text="item.title" />
          </v-list-item-content>
        </v-list-item>
      </v-list> -->
      <v-list>
        <v-subheader
          ><nuxt-link
            to="/"
            ><v-img
              style="max-width: 200px; background-color: white"
              src="logo.png" /></nuxt-link
        ></v-subheader>
        <v-list-group :value="true" prepend-icon="mdi-kodi">
          <template v-slot:activator>
            <v-list-item-title>営業管理</v-list-item-title>
          </template>

          <v-list-group :value="false" no-action sub-group>
            <template v-slot:activator>
              <v-list-item-content>
                <v-list-item-title>受注管理</v-list-item-title>
              </v-list-item-content>
            </template>

            <v-list-item
              v-for="([title, icon, to], i) in sales"
              :key="i"
              :to="to"
              link
            >
              <v-list-item-title
                v-text="title"
                :title="title"
              ></v-list-item-title>

              <v-list-item-icon>
                <v-icon v-text="icon"></v-icon>
              </v-list-item-icon>
            </v-list-item>
          </v-list-group>

          <v-list-group no-action sub-group>
            <template v-slot:activator>
              <v-list-item-content>
                <v-list-item-title>出荷管理</v-list-item-title>
              </v-list-item-content>
            </template>

            <v-list-item
              v-for="([title, icon, to], i) in shippings"
              :key="i"
              :to="to"
              link
            >
              <v-list-item-title
                v-text="title"
                :title="title"
              ></v-list-item-title>

              <v-list-item-icon>
                <v-icon v-text="icon"></v-icon>
              </v-list-item-icon>
            </v-list-item>
          </v-list-group>
        </v-list-group>
        <v-list-group :value="true" prepend-icon="mdi-warehouse">
          <template v-slot:activator>
            <v-list-item-title>倉庫管理</v-list-item-title>
          </template>

          <v-list-group :value="false" no-action sub-group>
            <template v-slot:activator>
              <v-list-item-content>
                <v-list-item-title>在庫照会</v-list-item-title>
              </v-list-item-content>
            </template>

            <v-list-item
              v-for="([title, icon, to], i) in inventories"
              :key="i"
              :to="to"
              link
            >
              <v-list-item-title
                v-text="title"
                :title="title"
              ></v-list-item-title>
              <v-list-item-icon>
                <v-icon v-text="icon"></v-icon>
              </v-list-item-icon>
            </v-list-item>
          </v-list-group>
        </v-list-group>
      </v-list>
    </v-navigation-drawer>
    <v-app-bar :clipped-left="clipped" fixed flat dense app>
      <v-app-bar-nav-icon @click.stop="drawer = !drawer" />
      <v-toolbar-title>
        <span style="font-size: 1rem">2021年11月10日&emsp;13:45</span>
      </v-toolbar-title>
      <!-- <v-btn
        icon
        @click.stop="miniVariant = !miniVariant"
      >
        <v-icon>mdi-{{ `chevron-${miniVariant ? 'right' : 'left'}` }}</v-icon>
      </v-btn>
      <v-btn
        icon
        @click.stop="clipped = !clipped"
      >
        <v-icon>mdi-application</v-icon>
      </v-btn> -->
      <!-- <v-btn
        icon
        @click.stop="fixed = !fixed"
      >
        <v-icon>mdi-minus</v-icon>
      </v-btn> -->
      <!-- <v-toolbar-title v-text="title" /> -->
      <v-spacer />
      <avater />      
      <!-- <v-btn
        icon
        @click.stop="rightDrawer = !rightDrawer"
      >
        <v-icon>mdi-menu</v-icon>
      </v-btn> -->
      <!-- <v-menu bottom min-width="200px" rounded offset-y>
        <template v-slot:activator="{ on }">
          <v-btn icon x-large v-on="on">
            <v-avatar color="primary" size="37">
              <span class="white--text text-h5">{{ user.initials }}</span>
            </v-avatar>
          </v-btn>
        </template>
        <v-card>
          <v-list-item-content class="justify-center">
            <div class="mx-auto text-center">
              <v-avatar color="primary">
                <span class="white--text text-h5">{{ user.initials }}</span>
              </v-avatar>
              <h3>{{ user.fullName }}</h3>
              <p class="text-caption mt-1">
                {{ user.email }}
              </p>
              <v-divider class="my-3"></v-divider>
              <v-btn depressed rounded text> プロファイル </v-btn>
              <v-divider class="my-3"></v-divider>
              <v-btn depressed rounded text> ログアウト </v-btn>
            </div>
          </v-list-item-content>
        </v-card>
      </v-menu> -->
    </v-app-bar>
    <v-main>
      <Nuxt />
    </v-main>
    <v-navigation-drawer v-model="rightDrawer" :right="right" temporary fixed>
      <v-list>
        <v-list-item @click.native="right = !right">
          <v-list-item-action>
            <v-icon light> mdi-repeat </v-icon>
          </v-list-item-action>
          <v-list-item-title>Switch drawer (click me)</v-list-item-title>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
    <v-footer :absolute="!fixed" app>
      <v-spacer />
      <button @click="returnTop">先頭へ戻る</button>
    </v-footer>
  </v-app>
</template>
<script>
export default {
  middleware: ['auth'],
  data() {
    return {
      clipped: false,
      drawer: false,
      fixed: true,
      user: {
        initials: 'N',
        fullName: 'NTTDATA SHINETSU',
        email: 'info@nttdata-shinetsu.com'
      },
      items: [
        {
          icon: 'mdi-apps',
          title: '',
          to: '/'
        },
        {
          icon: 'mdi-chart-bubble',
          title: 'Inspire',
          to: '/inspire'
        }
      ],
      sales: [
        ['SAMPLE', 'mdi-flask-outline', 'sample'],
        ['EXCEL', 'mdi-flask-outline', 'excel'],
        ['TEST', 'mdi-flask-outline', 'partstest'],
        ['A受注伝票入力', 'mdi-file-edit', 'efhj103a'],
        ['B受注伝票入力', 'mdi-file-edit', 'efhj103b'],
        ['受注伝票入力', 'mdi-file-edit', 'efhj1030'],
        ['受注伝票照会', 'mdi-file-search', 'efhj1010'],
        ['受注一括確定指示', 'mdi-file-document-edit'],
        ['契約条件検索', 'mdi-file-search', 'efhj1050'],
        ['受注・移動照会', 'mdi-file-search'],
        ['取引先登録', 'mdi-file-cog'],
        ['出荷先登録', 'mdi-file-cog'],
        ['単価登録', 'mdi-file-cog'],
        ['取引先出荷先属性登録', 'mdi-file-cog']
      ],
      shippings: [
        ['出荷予定表', 'mdi-file-eye-outline'],
        ['出荷予定入力', 'mdi-file-search'],
        ['輸送伝票出力', 'mdi-file-eye-outline'],
        ['荷札出力', 'mdi-file-search'],
        ['基幹用出荷伝票CSV', 'mdi-file-search'],
        ['出荷確定一括指示', 'mdi-file-settings'],
        ['出荷実績表出力', 'mdi-file-eye-outline'],
        ['返品明細表出力', 'mdi-file-eye-outline'],
        ['運賃入力', 'mdi-file-settings'],
        ['運賃日計表', 'mdi-file-settings'],
        ['運賃未払管理月報', 'mdi-file-settings'],
        ['ピッキングリスト出力指示', 'mdi-file-settings'],
        ['ピッキング状況照会', 'mdi-file-settings']
      ],
      inventories: [
        ['入出荷予定CSV作成', 'mdi-file-delimited'],
        ['在庫照会', 'mdi-file-search'],
        ['在庫振替', 'mdi-file-edit'],
        ['在庫移動', 'mdi-file-edit'],
        ['在庫移動（預）', 'mdi-file-edit'],
        ['在庫表', 'mdi-file-edit'],
        ['旬報出力', 'mdi-file-delimited'],
        ['品目在庫表CSV', 'mdi-file-delimited'],
        ['棚卸集計表（即時）', 'mdi-file-eye-outline'],
        ['棚卸集計表（月次）', 'mdi-file-eye-outline'],
        ['預り品受払データ', 'mdi-file-delimited'],
        ['現品データ作成', 'mdi-file-delimited'],
        ['テキスト送信状況', 'mdi-file-eye-outline'],
        ['出荷履歴照会', 'mdi-file-search'],
        ['ロット別在庫照会', 'mdi-file-search']
      ],
      miniVariant: false,
      right: true,
      rightDrawer: false,
      title: 'Vuetify.js'
    }
  },
  methods: {
    returnTop() {
      window.scrollTo({
        top: 0,
        behavior: 'smooth'
      })
    }
  }
}
</script>
<style>
label {
  line-height: 40px;
}
</style>
<style scoped>
.theme--light.v-application {
  /* background: #e0e0e0; */
  background: #F5F5F5;
  color: rgba(0, 0, 0, 0.87);
}
</style>
