<template>
    <div class="admin-page">
        <div class="admin-header">
            <h1>Admin Panel</h1>
            <a href="/">Back to CV Editor</a>
        </div>

        <div class="admin-section">
            <h2>CV Sections</h2>
            <p>Configure which sections are available to professors and in what order.</p>

            <div class="section-list">
                <div v-for="s in sectiuni" :key="s.id" class="section-row">
                    <div class="section-info">
                        <span class="section-title">{{ s.titlu }}</span>
                        <span class="section-slug">{{ s.slug }}</span>
                    </div>
                    <div class="section-controls">
                        <label>
                            <input type="checkbox" v-model="s.esteActiva" @change="save(s)" />
                            Active
                        </label>
                        <label>
                            <input type="checkbox" v-model="s.esteObligatorie" @change="save(s)" />
                            Required
                        </label>
                        <div class="order-controls">
                            <button @click="moveUp(s)" :disabled="s.ordine === 1">↑</button>
                            <button @click="moveDown(s)" :disabled="s.ordine === sectiuni.length">↓</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="admin-section">
            <h2>Professors</h2>
            <div class="section-list">
                <div v-for="p in profesori" :key="p.id" class="section-row">
                    <span>{{ p.prenume }} {{ p.nume }}</span>
                    <span class="section-slug">{{ p.email }}</span>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'

const API = 'https://localhost:7234'
const sectiuni = ref([])
const profesori = ref([])

async function save(s) {
    await axios.put(`${API}/api/Sectiuni/${s.id}`, s)
}

async function moveUp(s) {
    const idx = sectiuni.value.findIndex(x => x.id === s.id)
    if (idx === 0) return
    const other = sectiuni.value[idx - 1]
    const tempOrdine = s.ordine
    s.ordine = other.ordine
    other.ordine = tempOrdine
    sectiuni.value.sort((a, b) => a.ordine - b.ordine)
    await save(s)
    await save(other)
}

async function moveDown(s) {
    const idx = sectiuni.value.findIndex(x => x.id === s.id)
    if (idx === sectiuni.value.length - 1) return
    const other = sectiuni.value[idx + 1]
    const tempOrdine = s.ordine
    s.ordine = other.ordine
    other.ordine = tempOrdine
    sectiuni.value.sort((a, b) => a.ordine - b.ordine)
    await save(s)
    await save(other)
}

onMounted(async () => {
    const [sectRes, profRes] = await Promise.all([
        axios.get(`${API}/api/Sectiuni`),
        axios.get(`${API}/api/Profesori`)
    ])
    sectiuni.value = sectRes.data.sort((a, b) => a.ordine - b.ordine)
    profesori.value = profRes.data
})
</script>
