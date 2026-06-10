<template>
    <div class="admin-page">
        <div class="admin-header">
            <h1>Admin Panel</h1>
            <a href="/">Back to CV Editor</a>
        </div>

        <!-- Unified sections list -->
        <div class="admin-section">
            <h2>CV Sections</h2>
            <p>Configure which sections are available to professors and in what order. Built-in and custom sections share the same ordering.</p>
            <div class="section-list">
                <div v-for="item in allSections" :key="item.type + '-' + item.id"
                     class="section-row" :class="{ 'custom-section-row': item.type === 'custom' }">

                    <div class="custom-section-header">
                        <div class="section-info">
                            <span class="section-title">{{ item.titlu }}</span>
                            <span class="section-slug">{{ item.type === 'builtin' ? item.slug : (item.esteRepetabila ? 'repeatable' : 'single') }}</span>
                        </div>
                        <div class="section-controls">
                            <label>
                                <input type="checkbox" v-model="item.esteActiva"
                                       @change="item.type === 'builtin' ? save(item) : saveCustom(item)" />
                                Active
                            </label>
                            <label>
                                <input type="checkbox" v-model="item.esteObligatorie"
                                       @change="item.type === 'builtin' ? save(item) : saveCustom(item)" />
                                Required
                            </label>
                            <div class="order-controls">
                                <button @click="moveUp(item)" :disabled="allSections.indexOf(item) === 0">↑</button>
                                <button @click="moveDown(item)" :disabled="allSections.indexOf(item) === allSections.length - 1">↓</button>
                            </div>
                            <button v-if="item.type === 'custom'" class="btn-danger" @click="deleteCustomSection(item.id)">Delete</button>
                        </div>
                    </div>

                    <!-- Fields (custom sections only) -->
                    <div v-if="item.type === 'custom'" class="fields-list">
                        <div v-for="camp in item.campuri" :key="camp.id" class="field-row">
                            <span class="field-label">{{ camp.eticheta }}</span>
                            <span class="section-slug">{{ camp.tip }}</span>
                            <button class="btn-danger-sm" @click="deleteCamp(item, camp.id)">✕</button>
                        </div>

                        <div v-if="addingFieldFor === item.id" class="add-field-form">
                            <input v-model="newField.eticheta" placeholder="Field label" />
                            <select v-model="newField.tip">
                                <option value="text">Text</option>
                                <option value="textarea">Textarea</option>
                                <option value="number">Number</option>
                                <option value="date">Date</option>
                                <option value="url">URL</option>
                            </select>
                            <label>
                                <input type="checkbox" v-model="newField.esteFull" />
                                Full width
                            </label>
                            <button @click="addField(item)">Add</button>
                            <button @click="addingFieldFor = null">Cancel</button>
                        </div>
                        <button v-else class="btn-add-field" @click="startAddField(item.id)">+ Add Field</button>
                    </div>
                </div>
            </div>

            <!-- New custom section form -->
            <div v-if="showNewSectionForm" class="new-section-form">
                <input v-model="newSection.titlu" placeholder="Section name" />
                <label>
                    <input type="checkbox" v-model="newSection.esteRepetabila" />
                    Repeatable (professor can add multiple entries)
                </label>
                <button @click="createCustomSection">Create</button>
                <button @click="showNewSectionForm = false">Cancel</button>
            </div>
            <button v-else class="btn-add-section" @click="showNewSectionForm = true">+ New Section</button>
        </div>

        <!-- Professors list -->
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
import { ref, computed, onMounted } from 'vue'
import axios from 'axios'

const API = 'https://localhost:7234'
const sectiuni = ref([])
const sectiuniCustome = ref([])
const profesori = ref([])

const showNewSectionForm = ref(false)
const newSection = ref({ titlu: '', esteRepetabila: false })

const addingFieldFor = ref(null)
const newField = ref({ eticheta: '', tip: 'text', esteFull: false })

const allSections = computed(() =>
    [...sectiuni.value, ...sectiuniCustome.value].sort((a, b) => a.ordine - b.ordine)
)

async function save(s) {
    await axios.put(`${API}/api/Sectiuni/${s.id}`, s)
}

async function saveCustom(sc) {
    await axios.put(`${API}/api/SectiuniCustome/${sc.id}`, sc)
}

async function moveUp(item) {
    const all = allSections.value
    const idx = all.findIndex(x => x === item)
    if (idx === 0) return
    const other = all[idx - 1]
    ;[item.ordine, other.ordine] = [other.ordine, item.ordine]
    await (item.type === 'builtin' ? save(item) : saveCustom(item))
    await (other.type === 'builtin' ? save(other) : saveCustom(other))
}

async function moveDown(item) {
    const all = allSections.value
    const idx = all.findIndex(x => x === item)
    if (idx === all.length - 1) return
    const other = all[idx + 1]
    ;[item.ordine, other.ordine] = [other.ordine, item.ordine]
    await (item.type === 'builtin' ? save(item) : saveCustom(item))
    await (other.type === 'builtin' ? save(other) : saveCustom(other))
}

async function createCustomSection() {
    if (!newSection.value.titlu.trim()) return
    const res = await axios.post(`${API}/api/SectiuniCustome`, {
        titlu: newSection.value.titlu,
        esteActiva: true,
        esteObligatorie: false,
        esteRepetabila: newSection.value.esteRepetabila
    })
    sectiuniCustome.value.push({ ...res.data, type: 'custom' })
    newSection.value = { titlu: '', esteRepetabila: false }
    showNewSectionForm.value = false
}

async function deleteCustomSection(id) {
    if (!confirm('Delete this section and all its data?')) return
    await axios.delete(`${API}/api/SectiuniCustome/${id}`)
    sectiuniCustome.value = sectiuniCustome.value.filter(s => s.id !== id)
}

function startAddField(sectiuneId) {
    addingFieldFor.value = sectiuneId
    newField.value = { eticheta: '', tip: 'text', esteFull: false }
}

async function addField(sc) {
    if (!newField.value.eticheta.trim()) return
    const res = await axios.post(`${API}/api/SectiuniCustome/${sc.id}/campuri`, {
        eticheta: newField.value.eticheta,
        tip: newField.value.tip,
        esteFull: newField.value.esteFull
    })
    sc.campuri.push(res.data)
    addingFieldFor.value = null
}

async function deleteCamp(sc, campId) {
    if (!confirm('Delete this field? Existing data for this field will be lost.')) return
    await axios.delete(`${API}/api/SectiuniCustome/${sc.id}/campuri/${campId}`)
    sc.campuri = sc.campuri.filter(c => c.id !== campId)
}

onMounted(async () => {
    const [sectRes, customRes, profRes] = await Promise.all([
        axios.get(`${API}/api/Sectiuni`),
        axios.get(`${API}/api/SectiuniCustome`),
        axios.get(`${API}/api/Profesori`)
    ])
    sectiuni.value = sectRes.data.map(s => ({ ...s, type: 'builtin' }))
    sectiuniCustome.value = customRes.data.map(s => ({ ...s, type: 'custom' }))
    profesori.value = profRes.data
})
</script>
