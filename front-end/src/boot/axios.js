import Vue from 'vue'
import axios from 'axios'

axios.defaults.baseURL = 'https://localhost:5001/api/';
// axios.defaults.headers.common['Authorization'] = AUTH_TOKEN;
// axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
axios.defaults.headers.common['Content-Type'] = 'application/json';

Vue.prototype.$axios = axios
