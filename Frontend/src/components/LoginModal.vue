<template>
    <div class="modal-overlay" @click.self="$emit('close')">
      <div class="modal-card">
        <h2>Log in to save your CV</h2>
        <p>Enter your name to access your account</p>

        <form @submit.prevent="login">
          <input v-model="username" placeholder="First Last" autocomplete="off" />
          <p v-if="errorMsg" class="error-msg">{{ errorMsg }}</p>
          <button type="submit" :disabled="!username.trim()">Log in</button>
        </form>

        <button class="btn-guest" @click="$emit('close')">Continue as guest</button>
      </div>
    </div>
  </template>

   <script setup>
  import { ref } from 'vue'
  import { useRouter } from 'vue-router'
  import axios from 'axios'

  const emit = defineEmits(['close', 'loggedIn'])

  const username = ref('')
  const errorMsg = ref('')

  async function login() {
    try {
      const res = await axios.get('http://localhost:5138/api/Profesori/login', {
        params: { username: username.value.trim() }
      })
      emit('loggedIn', res.data)
      emit('close')
    } catch {
      errorMsg.value = 'Name not found.'
    }
  }
  </script>