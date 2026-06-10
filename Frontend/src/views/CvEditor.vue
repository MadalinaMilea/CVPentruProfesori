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
                <label>ResearchGate<input v-model="profiles.researchgate" /></label>
            </div>
        </div>

        <!-- All sections merged in admin-defined order -->
        <template v-for="item in editorSections" :key="item.type === 'builtin' ? item.slug : 'custom-' + item.sc.id">

            <!-- Built-in section -->
            <div v-if="item.type === 'builtin'" class="cv-section">
                <div class="section-header">
                    <h2>{{ item.slug }}</h2>
                    <div>
                        <button @click="moveUp(sectiuniActive.indexOf(item.slug))" :disabled="sectiuniActive.indexOf(item.slug) === 0">↑</button>
                        <button @click="moveDown(sectiuniActive.indexOf(item.slug))" :disabled="sectiuniActive.indexOf(item.slug) === sectiuniActive.length - 1">↓</button>
                        <button v-if="item.slug === 'publications' && profesor" @click="importPublications">Import from CROSSREF</button>
                        <button @click="addEntry(item.slug)">+</button>
                        <button @click="hideSection(item.slug)">✕</button>
                    </div>
                </div>

                <div v-for="(entry, i) in cv[item.slug]" :key="i" class="entry-card">
                    <button @click="cv[item.slug].splice(i, 1)">✕</button>
                    <template v-if="item.slug === 'education'">
                        <div class="fields-grid">
                            <label>{{ language === 'en' ? 'Institution' : 'Instituție' }}<input v-model="entry.institution" /></label>
                            <label>{{ language === 'en' ? 'Field of Study' : 'Domeniu' }}<input v-model="entry.area" /></label>
                            <label>{{ language === 'en' ? 'Degree Type' : 'Tip diplomă' }}<input v-model="entry.studyType" /></label>
                            <label>{{ language === 'en' ? 'Grade' : 'Notă' }}<input v-model="entry.score" /></label>
                            <label>{{ language === 'en' ? 'Start Date' : 'Data început' }}<input v-model="entry.startDate" /></label>
                            <label>{{ language === 'en' ? 'End Date' : 'Data sfârșit' }}<input v-model="entry.endDate" /></label>
                            <label class="full">{{ language === 'en' ? 'URL' : 'URL' }}<input v-model="entry.url" /></label>
                        </div>
                    </template>
                    <template v-else-if="item.slug === 'work'">
                        <div class="fields-grid">
                            <label>{{ language === 'en' ? 'Company' : 'Companie' }}<input v-model="entry.name" /></label>
                            <label>{{ language === 'en' ? 'Position' : 'Poziție' }}<input v-model="entry.position" /></label>
                            <label>{{ language === 'en' ? 'Start Date' : 'Data început' }}<input v-model="entry.startDate" /></label>
                            <label>{{ language === 'en' ? 'End Date' : 'Data sfârșit' }}<input v-model="entry.endDate" /></label>
                            <label class="full">{{ language === 'en' ? 'URL' : 'URL' }}<input v-model="entry.url" /></label>
                            <label class="full">{{ language === 'en' ? 'Description' : 'Descriere' }}<textarea v-model="entry.summary" rows="2"></textarea></label>
                        </div>
                    </template>
                    <template v-else-if="item.slug === 'publications'">
                        <div class="fields-grid">
                            <label>{{ language === 'en' ? 'Title' : 'Titlu' }}<input v-model="entry.name" /></label>
                            <label>{{ language === 'en' ? 'Publisher' : 'Editor' }}<input v-model="entry.publisher" /></label>
                            <label>{{ language === 'en' ? 'Date' : 'Data' }}<input v-model="entry.releaseDate" /></label>
                            <label class="full">{{ language === 'en' ? 'URL / DOI' : 'URL / DOI' }}<input v-model="entry.url" /></label>
                            <label class="full">{{ language === 'en' ? 'Abstract' : 'Rezumat' }}<textarea v-model="entry.summary" rows="2"></textarea></label>
                        </div>
                    </template>
                    <template v-else-if="item.slug === 'skills'">
                        <div class="fields-grid">
                            <label>{{ language === 'en' ? 'Skill' : 'Competență' }}<input v-model="entry.name" /></label>
                            <label>{{ language === 'en' ? 'Level' : 'Nivel' }}<input v-model="entry.level" /></label>
                        </div>
                    </template>
                    <template v-else-if="item.slug === 'languages'">
                        <div class="fields-grid">
                            <label>{{ language === 'en' ? 'Language' : 'Limbă' }}<input v-model="entry.language" /></label>
                            <label>{{ language === 'en' ? 'Fluency' : 'Nivel' }}<input v-model="entry.fluency" /></label>
                        </div>
                    </template>
                    <template v-else-if="item.slug === 'certificates'">
                        <div class="fields-grid">
                            <label>{{ language === 'en' ? 'Name' : 'Nume' }}<input v-model="entry.name" /></label>
                            <label>{{ language === 'en' ? 'Issuer' : 'Emitent' }}<input v-model="entry.issuer" /></label>
                            <label>{{ language === 'en' ? 'Date' : 'Data' }}<input v-model="entry.date" /></label>
                            <label class="full">{{ language === 'en' ? 'URL' : 'URL' }}<input v-model="entry.url" /></label>
                        </div>
                    </template>
                    <template v-else-if="item.slug === 'awards'">
                        <div class="fields-grid">
                            <label>{{ language === 'en' ? 'Title' : 'Titlu' }}<input v-model="entry.title" /></label>
                            <label>{{ language === 'en' ? 'Awarded by' : 'Acordat de' }}<input v-model="entry.awarder" /></label>
                            <label>{{ language === 'en' ? 'Date' : 'Data' }}<input v-model="entry.date" /></label>
                            <label class="full">{{ language === 'en' ? 'Description' : 'Descriere' }}<textarea v-model="entry.summary" rows="2"></textarea></label>
                        </div>
                    </template>
                    <template v-else-if="item.slug === 'projects'">
                        <div class="fields-grid">
                            <label>{{ language === 'en' ? 'Name' : 'Nume' }}<input v-model="entry.name" /></label>
                            <label>{{ language === 'en' ? 'Start Date' : 'Data început' }}<input v-model="entry.startDate" /></label>
                            <label>{{ language === 'en' ? 'End Date' : 'Data sfârșit' }}<input v-model="entry.endDate" /></label>
                            <label class="full">{{ language === 'en' ? 'URL' : 'URL' }}<input v-model="entry.url" /></label>
                            <label class="full">{{ language === 'en' ? 'Description' : 'Descriere' }}<textarea v-model="entry.description" rows="2"></textarea></label>
                        </div>
                    </template>
                    <template v-else-if="item.slug === 'interests'">
                        <div class="fields-grid">
                            <label>{{ language === 'en' ? 'Interest' : 'Interes' }}<input v-model="entry.name" /></label>
                        </div>
                    </template>
                    <template v-else-if="item.slug === 'references'">
                        <div class="fields-grid">
                            <label>{{ language === 'en' ? 'Name' : 'Nume' }}<input v-model="entry.name" /></label>
                            <label class="full">{{ language === 'en' ? 'Reference' : 'Referință' }}<textarea v-model="entry.reference" rows="2"></textarea></label>
                        </div>
                    </template>
                    <template v-else-if="item.slug === 'volunteer'">
                        <div class="fields-grid">
                            <label>{{ language === 'en' ? 'Organization' : 'Organizație' }}<input v-model="entry.organization" /></label>
                            <label>{{ language === 'en' ? 'Position' : 'Poziție' }}<input v-model="entry.position" /></label>
                            <label>{{ language === 'en' ? 'Start Date' : 'Data început' }}<input v-model="entry.startDate" /></label>
                            <label>{{ language === 'en' ? 'End Date' : 'Data sfârșit' }}<input v-model="entry.endDate" /></label>
                            <label class="full">{{ language === 'en' ? 'Description' : 'Descriere' }}<textarea v-model="entry.summary" rows="2"></textarea></label>
                        </div>
                    </template>
                </div>
            </div>

            <!-- Custom section -->
            <div v-else class="cv-section">
                <div class="section-header">
                    <h2>{{ item.sc.titlu }}</h2>
                    <div>
                        <button v-if="item.sc.esteRepetabila" @click="addCustomEntry(item.sc)">+</button>
                    </div>
                </div>

                <div v-for="(entry, ei) in (customValues[item.sc.id] || [])" :key="ei" class="entry-card">
                    <button
                        v-if="item.sc.esteRepetabila && (customValues[item.sc.id] || []).length > (item.sc.esteObligatorie ? 1 : 0)"
                        @click="removeCustomEntry(item.sc.id, ei)">✕</button>
                    <div class="fields-grid">
                        <label v-for="camp in item.sc.campuri" :key="camp.id" :class="{ full: camp.esteFull }">
                            {{ camp.eticheta }}
                            <textarea v-if="camp.tip === 'textarea'" v-model="entry.valori[camp.id]" rows="3"></textarea>
                            <input v-else :type="campInputType(camp.tip)" v-model="entry.valori[camp.id]" />
                        </label>
                    </div>
                </div>
            </div>

        </template>

    </div>

    <div class="fab-wrap">
        <div v-if="dropdownOpen" class="fab-menu">
            <button v-for="s in availableSections" :key="s.type + '-' + s.key" @click="addSection(s); dropdownOpen = false">
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

const API = 'https://localhost:7234'
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
    researchgate: ''
})

const sectiuniAdmin = ref([])
const sectiuniActive = ref([])
const dropdownOpen = ref(false)

// Custom sections
const customSections = ref([])
const customSectiuniActive = ref([]) // IDs of custom sections currently shown in editor
const customValues = ref({}) // sectiuneId → [{ id, ordine, valori: { campId: value } }]

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

function emptyCustomEntry(campuri) {
    const valori = {}
    campuri.forEach(c => { valori[c.id] = '' })
    return { id: null, ordine: 1, valori }
}

function campInputType(tip) {
    if (tip === 'number') return 'number'
    if (tip === 'date') return 'date'
    if (tip === 'url') return 'url'
    return 'text'
}

function addCustomEntry(sc) {
    if (!customValues.value[sc.id]) customValues.value[sc.id] = []
    const entry = emptyCustomEntry(sc.campuri)
    entry.ordine = customValues.value[sc.id].length + 1
    customValues.value[sc.id].push(entry)
}

function removeCustomEntry(sectiuneId, idx) {
    customValues.value[sectiuneId].splice(idx, 1)
}

function initCustomValues() {
    for (const sc of customSections.value) {
        if (!customSectiuniActive.value.includes(sc.id)) continue
        if (!customValues.value[sc.id] || customValues.value[sc.id].length === 0) {
            customValues.value[sc.id] = [emptyCustomEntry(sc.campuri)]
        }
    }
}

function toggleLanguage() {
    language.value = language.value === 'en' ? 'ro' : 'en'
}

async function saveCV() {
    cv.value.basics.profiles = Object.entries(profiles.value)
        .filter(([, url]) => url.trim())
        .map(([key, url]) => ({ network: key, url: url.trim() }))
    await axios.put(`${API}/api/Profesori/${profesor.value.id}/cv`, cv.value)

    const payload = Object.entries(customValues.value).map(([sectiuneId, entries]) => ({
        sectiuneId: parseInt(sectiuneId),
        inregistrari: entries.map((entry, idx) => ({
            id: entry.id,
            ordine: idx + 1,
            valori: Object.entries(entry.valori).map(([campId, valoare]) => ({
                campId: parseInt(campId),
                valoare: valoare || ''
            }))
        }))
    }))
    await axios.put(`${API}/api/Profesori/${profesor.value.id}/sectiuniCustome`, payload)

    alert('CV saved!')
}

async function exportPDF() {
    const [sectRes, customRes] = await Promise.all([
        axios.get(`${API}/api/Sectiuni/active`),
        axios.get(`${API}/api/SectiuniCustome/active`)
    ])
    const freshBuiltin = sectRes.data
    const freshCustom = customRes.data

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

    if (b.profiles && b.profiles.length) {
        const profileParts = b.profiles.filter(p => p.network && p.url).map(p => `${p.network}: ${p.url}`)
        if (profileParts.length) {
            doc.setFontSize(8); doc.setFont('helvetica', 'normal'); doc.setTextColor(...GREY)
            const profileLines = doc.splitTextToSize(profileParts.join('   |   '), CONTENT_W)
            doc.text(profileLines, ML, y)
            y += profileLines.length * (8 * 0.42) + 2
        }
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

    const builtinForPdf = freshBuiltin
        .filter(s => sectiuniActive.value.includes(s.slug))
        .map(s => ({ type: 'builtin', slug: s.slug, ordine: s.ordine }))
    const customForPdf = freshCustom
        .map(s => ({ type: 'custom', id: s.id, ordine: s.ordine, titlu: s.titlu, campuri: s.campuri }))
    const allForPdf = [...builtinForPdf, ...customForPdf].sort((a, b) => a.ordine - b.ordine)

    for (const section of allForPdf) {
        if (section.type === 'builtin') {
            if (!cv.value[section.slug]?.length) continue
            sectionHeader(labels[language.value][section.slug] || section.slug)
            for (const entry of cv.value[section.slug]) { renderers[section.slug]?.(entry); y += 2 }
        } else {
            const entries = customValues.value[section.id] || []
            const hasData = entries.some(e => Object.values(e.valori).some(v => v && String(v).trim()))
            if (!hasData) continue
            sectionHeader(section.titlu)
            for (const entry of entries) {
                for (const camp of section.campuri) {
                    const val = entry.valori[camp.id]
                    if (val && String(val).trim()) {
                        entryRow(camp.eticheta, '')
                        txt(String(val), 9, false, GREY)
                    }
                }
                y += 2
            }
        }
    }

    doc.save(`CV_${name}.pdf`)
}

async function importPublications() {
    const res = await axios.get(`${API}/api/Publicatii/import/${profesor.value.id}`)
    const fetched = res.data
    const existing = cv.value.publications || []
    const existingTitles = existing.map(p => (p.name || '').toLowerCase())
    const toAdd = fetched.filter(p => !existingTitles.includes((p.name || '').toLowerCase()))
    cv.value.publications = [...existing, ...toAdd]
    if (!sectiuniActive.value.includes('publications')) sectiuniActive.value.push('publications')
    alert(toAdd.length + ' new publications imported.')
}

function logout() {
    sessionStorage.removeItem('profesor')
    profesor.value = null
    cv.value = emptyCV()
    profiles.value = { orcid: '', googleScholar: '', wos: '', researchgate: '' }
    customValues.value = {}
    const required = sectiuniAdmin.value.filter(s => s.esteObligatorie).map(s => s.slug)
    sectiuniActive.value = [...required]
    for (const slug of required) {
        cv.value[slug].push(emptyEntry(slug))
    }
    customSectiuniActive.value = customSections.value.filter(sc => sc.esteObligatorie).map(sc => sc.id)
    initCustomValues()
}

async function onLoggedIn(profesorData) {
    profesor.value = profesorData
    sessionStorage.setItem('profesor', JSON.stringify(profesorData))
    await loadCV(profesorData.id)
}

async function loadCV(id) {
    const [cvRes, customRes] = await Promise.all([
        axios.get(`${API}/api/Profesori/${id}/cv`),
        axios.get(`${API}/api/Profesori/${id}/sectiuniCustome`)
    ])

    const data = typeof cvRes.data === 'string' ? JSON.parse(cvRes.data) : cvRes.data
    cv.value = { ...emptyCV(), ...data }

    for (const p of (data.basics?.profiles || [])) {
        const key = Object.keys(profiles.value).find(k => k.toLowerCase() === p.network?.toLowerCase())
        if (key) profiles.value[key] = p.url || ''
    }

    const withData = sectiuniAdmin.value.map(s => s.slug).filter(slug => cv.value[slug]?.length > 0)
    const required = sectiuniAdmin.value.filter(s => s.esteObligatorie).map(s => s.slug)
    sectiuniActive.value = [...new Set([...required, ...withData])]

    for (const slug of required) {
        if (!cv.value[slug].length) cv.value[slug].push(emptyEntry(slug))
    }

    // Load custom values
    const values = {}
    for (const item of customRes.data) {
        values[item.sectiuneId] = item.inregistrari.map(inr => ({
            id: inr.id,
            ordine: inr.ordine,
            valori: Object.fromEntries(inr.valori.map(v => [v.campId, v.valoare]))
        }))
    }
    customValues.value = values
    const requiredCustom = customSections.value.filter(sc => sc.esteObligatorie).map(sc => sc.id)
    const customWithData = Object.keys(values).map(Number).filter(id => values[id].length > 0)
    customSectiuniActive.value = [...new Set([...requiredCustom, ...customWithData])]
    initCustomValues()
}

function addSection(item) {
    if (item.type === 'builtin') {
        if (!sectiuniActive.value.includes(item.key)) {
            sectiuniActive.value.push(item.key)
            if (!cv.value[item.key].length) cv.value[item.key].push(emptyEntry(item.key))
        }
    } else {
        if (!customSectiuniActive.value.includes(item.key)) {
            customSectiuniActive.value.push(item.key)
            const sc = customSections.value.find(s => s.id === item.key)
            if (sc && (!customValues.value[item.key] || !customValues.value[item.key].length))
                customValues.value[item.key] = [emptyCustomEntry(sc.campuri)]
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

const availableSections = computed(() => {
    const inactiveBuiltin = sectiuniAdmin.value
        .filter(s => !sectiuniActive.value.includes(s.slug))
        .map(s => ({ type: 'builtin', key: s.slug, titlu: s.titlu }))
    const inactiveCustom = customSections.value
        .filter(sc => !customSectiuniActive.value.includes(sc.id))
        .map(sc => ({ type: 'custom', key: sc.id, titlu: sc.titlu }))
    return [...inactiveBuiltin, ...inactiveCustom]
})

const editorSections = computed(() => {
    const sortedBuiltin = sectiuniActive.value
        .map(slug => sectiuniAdmin.value.find(s => s.slug === slug))
        .filter(Boolean)
    const sortedCustom = [...customSections.value]
        .filter(sc => customSectiuniActive.value.includes(sc.id))
        .sort((a, b) => a.ordine - b.ordine)

    const result = []
    let ci = 0
    for (const s of sortedBuiltin) {
        while (ci < sortedCustom.length && sortedCustom[ci].ordine < s.ordine) {
            result.push({ type: 'custom', sc: sortedCustom[ci] })
            ci++
        }
        result.push({ type: 'builtin', slug: s.slug, ordine: s.ordine })
    }
    while (ci < sortedCustom.length) {
        result.push({ type: 'custom', sc: sortedCustom[ci++] })
    }
    return result
})

onMounted(async () => {
    const stored = sessionStorage.getItem('profesor')
    if (stored) profesor.value = JSON.parse(stored)
    cv.value = emptyCV()

    const [sectRes, customRes] = await Promise.all([
        axios.get(`${API}/api/Sectiuni/active`),
        axios.get(`${API}/api/SectiuniCustome/active`)
    ])
    sectiuniAdmin.value = sectRes.data
    customSections.value = customRes.data

    const required = sectiuniAdmin.value.filter(s => s.esteObligatorie).map(s => s.slug)
    sectiuniActive.value = [...required]
    for (const slug of required) {
        cv.value[slug].push(emptyEntry(slug))
    }
    customSectiuniActive.value = customSections.value.filter(sc => sc.esteObligatorie).map(sc => sc.id)

    initCustomValues()

    if (profesor.value) await loadCV(profesor.value.id)
})
</script>
