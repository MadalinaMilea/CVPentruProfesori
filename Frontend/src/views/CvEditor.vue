<template>
    <div class="editor-page">

        <!-- Top bar -->
        <div class="topbar">
            <h1>CV Editor</h1>
            <div class="topbar-actions">
                <button @click="toggleLanguage">{{ language === 'en' ? 'RO' : 'EN' }}</button>
                <button v-if="!profesor" @click="showLoginModal = true">Log in to save your CV</button>
                <template v-else>
                    <button @click="saveCV">Save</button>
                    <button @click="exportPDF">Export PDF</button>
                    <button @click="logout">Sign out</button>
                </template>
            </div>
        </div>

        <LoginModal v-if="showLoginModal" @close="showLoginModal = false" @loggedIn="onLoggedIn" />

        <div class="cv-section">
            <h2>{{ language === 'en' ? 'Basic Information' : 'Informații de bază' }}</h2>
            <div class="fields-grid">
                <label>{{ language === 'en' ? 'Name' : 'Nume' }}<input v-model="cv.basics.name" /></label>
                <label>{{ language === 'en' ? 'Title / Position' : 'Titlu / Poziție' }}<input
                        v-model="cv.basics.label" /></label>
                <label>{{ language === 'en' ? 'Email' : 'Email' }}<input v-model="cv.basics.email" /></label>
                <label>{{ language === 'en' ? 'Phone' : 'Telefon' }}<input v-model="cv.basics.phone" /></label>
                <label>{{ language === 'en' ? 'Website' : 'Website' }}<input v-model="cv.basics.url" /></label>
                <label class="full">{{ language === 'en' ? 'Summary' : 'Rezumat' }}<textarea v-model="cv.basics.summary"
                        rows="3"></textarea></label>
            </div>
        </div>

        <div class="cv-section">
            <h2>{{ language === 'en' ? 'Academic Profiles' : 'Profiluri Academice' }}</h2>
            <div class="fields-grid">
                <label>ORCID<input v-model="profiles.orcid" /></label>
                <label>Google Scholar<input v-model="profiles.googleScholar" /></label>
                <label>Web of Science<input v-model="profiles.wos" /></label>
                <label>CROSSREF<input v-model="profiles.crossref" /></label>
                <label>ResearchGate<input v-model="profiles.researchgate" /></label>
            </div>
        </div>

        <div v-for="(slug, idx) in sectiuniActive" :key="slug" class="cv-section">
            <div class="section-header">
                <h2>{{ slug }}</h2>
                <div>
                    <button @click="moveUp(idx)" :disabled="idx === 0">↑</button>
                    <button @click="moveDown(idx)" :disabled="idx === sectiuniActive.length - 1">↓</button>
                    <button @click="addEntry(slug)">+</button>
                    <button @click="hideSection(slug)">✕</button>
                </div>
            </div>

            <div v-for="(entry, i) in cv[slug]" :key="i" class="entry-card">
                <button @click="cv[slug].splice(i, 1)">✕</button>
                <template v-if="slug === 'education'">
                    <div class="fields-grid">
                        <label>{{ language === 'en' ? 'Institution' : 'Instituție' }}<input
                                v-model="entry.institution" /></label>
                        <label>{{ language === 'en' ? 'Field of Study' : 'Domeniu' }}<input
                                v-model="entry.area" /></label>
                        <label>{{ language === 'en' ? 'Degree Type' : 'Tip diplomă' }}<input
                                v-model="entry.studyType" /></label>
                        <label>{{ language === 'en' ? 'Grade' : 'Notă' }}<input v-model="entry.score" /></label>
                        <label>{{ language === 'en' ? 'Start Date' : 'Data început' }}<input
                                v-model="entry.startDate" /></label>
                        <label>{{ language === 'en' ? 'End Date' : 'Data sfârșit' }}<input
                                v-model="entry.endDate" /></label>
                        <label class="full">{{ language === 'en' ? 'URL' : 'URL' }}<input v-model="entry.url" /></label>
                    </div>
                </template>
                <template v-else-if="slug === 'work'">
                    <div class="fields-grid">
                        <label>{{ language === 'en' ? 'Company' : 'Companie' }}<input v-model="entry.name" /></label>
                        <label>{{ language === 'en' ? 'Position' : 'Poziție' }}<input
                                v-model="entry.position" /></label>
                        <label>{{ language === 'en' ? 'Start Date' : 'Data început' }}<input
                                v-model="entry.startDate" /></label>
                        <label>{{ language === 'en' ? 'End Date' : 'Data sfârșit' }}<input
                                v-model="entry.endDate" /></label>
                        <label class="full">{{ language === 'en' ? 'URL' : 'URL' }}<input v-model="entry.url" /></label>
                        <label class="full">{{ language === 'en' ? 'Description' : 'Descriere' }}<textarea
                                v-model="entry.summary" rows="2"></textarea></label>
                    </div>
                </template>
                <template v-else-if="slug === 'publications'">
                    <div class="fields-grid">
                        <label>{{ language === 'en' ? 'Title' : 'Titlu' }}<input v-model="entry.name" /></label>
                        <label>{{ language === 'en' ? 'Publisher' : 'Editor' }}<input
                                v-model="entry.publisher" /></label>
                        <label>{{ language === 'en' ? 'Date' : 'Data' }}<input v-model="entry.releaseDate" /></label>
                        <label class="full">{{ language === 'en' ? 'URL / DOI' : 'URL / DOI' }}<input
                                v-model="entry.url" /></label>
                        <label class="full">{{ language === 'en' ? 'Abstract' : 'Rezumat' }}<textarea
                                v-model="entry.summary" rows="2"></textarea></label>
                    </div>
                </template>

                <!-- Skills -->
                <template v-else-if="slug === 'skills'">
                    <div class="fields-grid">
                        <label>{{ language === 'en' ? 'Skill' : 'Competență' }}<input v-model="entry.name" /></label>
                        <label>{{ language === 'en' ? 'Level' : 'Nivel' }}<input v-model="entry.level" /></label>
                    </div>
                </template>

                <!-- Languages -->
                <template v-else-if="slug === 'languages'">
                    <div class="fields-grid">
                        <label>{{ language === 'en' ? 'Language' : 'Limbă' }}<input v-model="entry.language" /></label>
                        <label>{{ language === 'en' ? 'Fluency' : 'Nivel' }}<input v-model="entry.fluency" /></label>
                    </div>
                </template>

                <!-- Certificates -->
                <template v-else-if="slug === 'certificates'">
                    <div class="fields-grid">
                        <label>{{ language === 'en' ? 'Name' : 'Nume' }}<input v-model="entry.name" /></label>
                        <label>{{ language === 'en' ? 'Issuer' : 'Emitent' }}<input v-model="entry.issuer" /></label>
                        <label>{{ language === 'en' ? 'Date' : 'Data' }}<input v-model="entry.date" /></label>
                        <label class="full">{{ language === 'en' ? 'URL' : 'URL' }}<input v-model="entry.url" /></label>
                    </div>
                </template>

                <!-- Awards -->
                <template v-else-if="slug === 'awards'">
                    <div class="fields-grid">
                        <label>{{ language === 'en' ? 'Title' : 'Titlu' }}<input v-model="entry.title" /></label>
                        <label>{{ language === 'en' ? 'Awarded by' : 'Acordat de' }}<input
                                v-model="entry.awarder" /></label>
                        <label>{{ language === 'en' ? 'Date' : 'Data' }}<input v-model="entry.date" /></label>
                        <label class="full">{{ language === 'en' ? 'Description' : 'Descriere' }}<textarea
                                v-model="entry.summary" rows="2"></textarea></label>
                    </div>
                </template>

                <!-- Projects -->
                <template v-else-if="slug === 'projects'">
                    <div class="fields-grid">
                        <label>{{ language === 'en' ? 'Name' : 'Nume' }}<input v-model="entry.name" /></label>
                        <label>{{ language === 'en' ? 'Start Date' : 'Data început' }}<input
                                v-model="entry.startDate" /></label>
                        <label>{{ language === 'en' ? 'End Date' : 'Data sfârșit' }}<input
                                v-model="entry.endDate" /></label>
                        <label class="full">{{ language === 'en' ? 'URL' : 'URL' }}<input v-model="entry.url" /></label>
                        <label class="full">{{ language === 'en' ? 'Description' : 'Descriere' }}<textarea
                                v-model="entry.description" rows="2"></textarea></label>
                    </div>
                </template>

                <!-- Interests -->
                <template v-else-if="slug === 'interests'">
                    <div class="fields-grid">
                        <label>{{ language === 'en' ? 'Interest' : 'Interes' }}<input v-model="entry.name" /></label>
                    </div>
                </template>

                <!-- References -->
                <template v-else-if="slug === 'references'">
                    <div class="fields-grid">
                        <label>{{ language === 'en' ? 'Name' : 'Nume' }}<input v-model="entry.name" /></label>
                        <label class="full">{{ language === 'en' ? 'Reference' : 'Referință' }}<textarea
                                v-model="entry.reference" rows="2"></textarea></label>
                    </div>
                </template>

                <!-- Volunteer -->
                <template v-else-if="slug === 'volunteer'">
                    <div class="fields-grid">
                        <label>{{ language === 'en' ? 'Organization' : 'Organizație' }}<input
                                v-model="entry.organization" /></label>
                        <label>{{ language === 'en' ? 'Position' : 'Poziție' }}<input
                                v-model="entry.position" /></label>
                        <label>{{ language === 'en' ? 'Start Date' : 'Data început' }}<input
                                v-model="entry.startDate" /></label>
                        <label>{{ language === 'en' ? 'End Date' : 'Data sfârșit' }}<input
                                v-model="entry.endDate" /></label>
                        <label class="full">{{ language === 'en' ? 'Description' : 'Descriere' }}<textarea
                                v-model="entry.summary" rows="2"></textarea></label>
                    </div>
                </template>
            </div>
        </div>

    </div>

    <div class="fab-wrap">
        <div v-if="dropdownOpen" class="fab-menu">
            <button v-for="s in availableSections" :key="s.slug" @click="addSection(s.slug); dropdownOpen = false">
                + {{ s.titlu }}
            </button>
            <p v-if="!availableSections.length">All sections added</p>
        </div>
        <button class="fab" @click="dropdownOpen = !dropdownOpen">
            {{ dropdownOpen ? '×' : '+' }}
        </button>
    </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import LoginModal from '../components/LoginModal.vue'

const language = ref('en')
const profesor = ref(null)
const showLoginModal = ref(false)

const cv = ref({
    basics: { name: '', label: '', email: '', phone: '', url: '', summary: '' },
    work: [],
    education: [],
    publications: [],
    skills: [],
    languages: [],
    certificates: [],
    projects: [],
    awards: [],
    interests: [],
    references: [],
    volunteer: []
})

const profiles = ref({
    orcid: '',
    googleScholar: '',
    wos: '',
    crossref: '',
    researchgate: ''
})

const sectiuniAdmin = ref([])
const sectiuniActive = ref([])
const dropdownOpen = ref(false)

function emptyCV() {
    return {
        basics: { name: '', label: '', email: '', phone: '', url: '', summary: '' },
        work: [],
        education: [],
        publications: [],
        skills: [],
        languages: [],
        certificates: [],
        projects: [],
        awards: [],
        interests: [],
        references: [],
        volunteer: []
    }
}

function emptyEntry(slug) {
    const map = {
        work: { name: '', position: '', url: '', startDate: '', endDate: '', summary: '', highlights: [] },
        volunteer: { organization: '', position: '', url: '', startDate: '', endDate: '', summary: '', highlights: [] },
        education: { institution: '', url: '', area: '', studyType: '', startDate: '', endDate: '', score: '' },
        publications: { name: '', publisher: '', releaseDate: '', url: '', summary: '' },
        skills: { name: '', level: '', keywords: [] },
        languages: { language: '', fluency: '' },
        certificates: { name: '', date: '', issuer: '', url: '' },
        projects: { name: '', description: '', url: '', startDate: '', endDate: '', highlights: [] },
        awards: { title: '', date: '', awarder: '', summary: '' },
        interests: { name: '', keywords: [] },
        references: { name: '', reference: '' },
    }
    return { ...map[slug] }
}

function toggleLanguage() {
    language.value = language.value === 'en' ? 'ro' : 'en'
}

function saveCV() { }
function exportPDF() { }
function logout() { }

function onLoggedIn(profesorData) {
    profesor.value = profesorData
    sessionStorage.setItem('profesor', JSON.stringify(profesorData))
  }

function addSection(slug) {
    if (!sectiuniActive.value.includes(slug)) {
        sectiuniActive.value.push(slug)
        if (!cv.value[slug].length) {
            cv.value[slug].push(emptyEntry(slug))
        }
    }
}

function addEntry(slug) {
    cv.value[slug].push(emptyEntry(slug))
}

function hideSection(slug) {
    sectiuniActive.value = sectiuniActive.value.filter(s => s !== slug)
}

function moveUp(idx) {
    if (idx === 0) return
    const arr = [...sectiuniActive.value]
        ;[arr[idx - 1], arr[idx]] = [arr[idx], arr[idx - 1]]
    sectiuniActive.value = arr
}

function moveDown(idx) {
    if (idx === sectiuniActive.value.length - 1) return
    const arr = [...sectiuniActive.value]
        ;[arr[idx], arr[idx + 1]] = [arr[idx + 1], arr[idx]]
    sectiuniActive.value = arr
}

const availableSections = computed(() =>
    sectiuniAdmin.value.filter(s => !sectiuniActive.value.includes(s.slug))
)

onMounted(() => {
    const stored = sessionStorage.getItem('profesor')
    if (stored) profesor.value = JSON.parse(stored)
    cv.value = emptyCV()

    sectiuniAdmin.value = [
        { slug: 'education', titlu: 'Education', esteObligatoriu: true },
        { slug: 'work', titlu: 'Work Experience', esteObligatoriu: false },
        { slug: 'skills', titlu: 'Skills', esteObligatoriu: false },
        { slug: 'publications', titlu: 'Publications', esteObligatoriu: false },
        { slug: 'languages', titlu: 'Languages', esteObligatoriu: false },
    ]

    sectiuniActive.value = ['education']
    cv.value.education.push(emptyEntry('education'))
})

</script>