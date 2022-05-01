<template>
  <div class="vue-tempalte" align="center">
    <b-col lg="3">
      <b-card :title="('Create new user')">
        <form @submit.prevent="createRecord">
          <b-form-group label="Full name">
            <b-form-input type="text" ref="fullname" v-model="model.fullname"></b-form-input>
          </b-form-group>
          <b-form-group label="Username">
            <b-form-input rows="4" ref="username" v-model="model.username"></b-form-input>
          </b-form-group>
          <b-form-group label="Password">
            <b-form-input rows="4" ref="password" v-model="model.password" type="password"></b-form-input>
          </b-form-group>
          <div>
            <b-btn type="submit" variant="success">Create</b-btn>
          </div>
        </form>
      </b-card>
    </b-col>
  </div>
</template>

<script>
  import api from '@/UsersAPIService';

  export default {
    data() {
      return {
        loading: false,
        records: [],
        model: {}
      };
    },
    methods: {
      async createRecord() {
        await api.create(this.model)
      },

    }
  }
</script>
