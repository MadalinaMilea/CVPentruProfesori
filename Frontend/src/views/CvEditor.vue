<template>
    <div class="editor-page">

        <!-- Top bar -->
        <div class="topbar">
            <h1>CV Editor</h1>
            <div class="topbar-actions">
                <button @click="toggleLanguage">{{ language === 'en' ? 'RO' : 'EN' }}</button>
                <button @click="exportPDF">Export PDF</button>
                <button v-if="!profesor" @click="showLoginModal = true">Log in to save your CV</button>
                <template v-else>
                    <button @click="saveCV">Save</button>
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
import axios from 'axios'
import { jsPDF } from 'jspdf'

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

async function saveCV() {
    cv.value.basics.profiles = Object.entries(profiles.value)
        .filter(([, url]) => url.trim())
        .map(([key, url]) => ({
            network: key,
            url: url.trim()
        }))
    await axios.put(`https://localhost:7234/api/Profesori/${profesor.value.id}/cv`, cv.value)
    alert('CV saved!')
}
function exportPDF() {
    const doc = new jsPDF({ unit: 'mm', format: 'a4' })

    const ML = 18, MR = 18, PAGE_W = 210, PAGE_H = 297
    const CONTENT_W = PAGE_W - ML - MR
    const NAVY = [26, 39, 68], ACCENT = [26, 71, 100], DARK = [30, 30, 30], GREY = [100, 100, 100], LIGHT = [180, 180, 180]

    let y = 16

    const checkPage = (needed = 12) => {
        if (y + needed > PAGE_H - 12) { doc.addPage(); y = 16 }
    }

    const txt = (text, size = 10, bold = false, color = DARK) => {
        if (!text) return
        doc.setFontSize(size); doc.setFont('helvetica', bold ? 'bold' : 'normal'); doc.setTextColor(...color)
        const lines = doc.splitTextToSize(String(text), CONTENT_W)
        checkPage(lines.length * size * 0.45)
        doc.text(lines, ML, y)
        y += lines.length * (size * 0.42) + 1.5
    }

    const sectionHeader = (title) => {
        checkPage(14); y += 5
        doc.setFillColor(...ACCENT); doc.rect(ML, y - 4.5, 3, 6, 'F')
        doc.setFontSize(10.5); doc.setFont('helvetica', 'bold'); doc.setTextColor(...NAVY)
        doc.text(title.toUpperCase(), ML + 5, y)
        doc.setDrawColor(...LIGHT); doc.setLineWidth(0.3)
        doc.line(ML + 5, y + 1.5, PAGE_W - MR, y + 1.5)
        y += 6
    }

    const entryRow = (left, right) => {
        checkPage(8)
        const rightW = 42
        doc.setFontSize(10); doc.setFont('helvetica', 'bold'); doc.setTextColor(...DARK)
        const lines = doc.splitTextToSize(left, CONTENT_W - rightW)
        doc.text(lines, ML, y)
        if (right) {
            doc.setFontSize(8.5); doc.setFont('helvetica', 'normal'); doc.setTextColor(...GREY)
            doc.text(right, PAGE_W - MR, y, { align: 'right' })
        }
        y += lines.length * (10 * 0.42) + 1.5
    }

    // Header
    const b = cv.value.basics
    const name = b.name || 'CV'
    doc.setFontSize(22); doc.setFont('helvetica', 'bold'); doc.setTextColor(...NAVY)
    doc.text(name, ML, y); y += 9

    if (b.label) {
        doc.setFontSize(11); doc.setFont('helvetica', 'normal'); doc.setTextColor(...ACCENT)
        doc.text(b.label, ML, y); y += 6
    }

    const contactParts = [b.email, b.phone, b.url].filter(Boolean)
    if (contactParts.length) {
        doc.setFontSize(8.5); doc.setFont('helvetica', 'normal'); doc.setTextColor(...GREY)
        doc.text(contactParts.join('   |   '), ML, y); y += 5
    }

    y += 2; doc.setDrawColor(...NAVY); doc.setLineWidth(0.6); doc.line(ML, y, PAGE_W - MR, y); y += 6

    if (b.summary) { sectionHeader('Summary'); txt(b.summary, 9.5, false, GREY); y += 2 }

    const labels = {
        en: { work: 'Work Experience', education: 'Education', skills: 'Skills', languages: 'Languages', publications: 'Publications', certificates: 'Certificates', projects: 'Projects', awards: 'Awards', interests: 'Interests', references: 'References', volunteer: 'Volunteer' },
        ro: { work: 'Experiență profesională', education: 'Educație', skills: 'Competențe', languages: 'Limbi', publications: 'Publicații', certificates: 'Certificate', projects: 'Proiecte', awards: 'Premii', interests: 'Interese', references: 'Referințe', volunteer: 'Voluntariat' }
    }

    const renderers = {
        work: (e) => { entryRow([e.position, e.name].filter(Boolean).join(' — '), [e.startDate, e.endDate].filter(Boolean).join(' – ')); if (e.summary) txt(e.summary, 9, false, GREY) },
        volunteer: (e) => { entryRow([e.position, e.organization].filter(Boolean).join(' — '), [e.startDate, e.endDate].filter(Boolean).join(' – ')); if (e.summary) txt(e.summary, 9, false, GREY) },
        education: (e) => { entryRow([e.studyType, e.area].filter(Boolean).join(' in '), [e.startDate, e.endDate].filter(Boolean).join(' – ')); if (e.institution) txt(e.institution, 9, false, GREY) },
        publications: (e) => { entryRow(e.name || '', e.releaseDate || ''); if (e.publisher) txt(e.publisher, 9, false, GREY); if (e.url) txt(e.url, 8, false, [0, 100, 180]) },
        skills: (e) => { txt(`• ${e.name}${e.level ? ' — ' + e.level : ''}`, 9.5, false, DARK) },
        languages: (e) => { txt(`• ${e.language}${e.fluency ? ' — ' + e.fluency : ''}`, 9.5, false, DARK) },
        certificates: (e) => { entryRow(e.name || '', e.date || ''); if (e.issuer) txt(e.issuer, 9, false, GREY) },
        projects: (e) => { entryRow(e.name || '', [e.startDate, e.endDate].filter(Boolean).join(' – ')); if (e.description) txt(e.description, 9, false, GREY) },
        awards: (e) => { entryRow(e.title || '', e.date || ''); if (e.awarder) txt(e.awarder, 9, false, GREY) },
        interests: (e) => { txt(`• ${e.name}`, 9.5, false, DARK) },
        references: (e) => { entryRow(e.name || '', ''); if (e.reference) txt(e.reference, 9, false, GREY) },
    }

    for (const slug of sectiuniActive.value) {
        if (!cv.value[slug]?.length) continue
        sectionHeader(labels[language.value][slug] || slug)
        for (const entry of cv.value[slug]) { renderers[slug]?.(entry); y += 2 }
    }

    doc.save(`CV_${name}.pdf`)
}
function logout() {
    sessionStorage.removeItem('profesor')
    profesor.value = null
}

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

onMounted(async () => {
    const stored = sessionStorage.getItem('profesor')
    if (stored) profesor.value = JSON.parse(stored)
    cv.value = emptyCV()

    const res = await axios.get('https://localhost:7234/api/Sectiuni/active')
    sectiuniAdmin.value = res.data
    const required = sectiuniAdmin.value.filter(s => s.esteObligatorie).map(s => s.slug)
    sectiuniActive.value = [...required]
    for (const slug of required) {
        cv.value[slug].push(emptyEntry(slug))
    }
})

</script>