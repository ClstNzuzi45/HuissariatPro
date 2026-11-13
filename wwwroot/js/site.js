/* ============================================================
   HUISSIERS DE JUSTICE • RDC — Site public
   Vérification via API ASP.NET : /api/verification/...
   Fallback démo si l'API est indisponible.
   ============================================================ */
(function () {
    'use strict';

    // ===== DONNÉES DÉMO (fallback si API KO) =====
    const HUISSIERS_DEMO = [
        { nomComplet: "Kabila Jean", matricule: "HJ-2026-0001", ville: "Kinshasa", province: "Kinshasa", etude: "Étude Maître Kabila", telephone: "+243 900 111 222", statut: "Actif" },
        { nomComplet: "Mbuyi Sarah", matricule: "HJ-2026-0002", ville: "Lubumbashi", province: "Haut-Katanga", etude: "Étude Mbuyi & Associés", telephone: "+243 900 333 444", statut: "Actif" },
        { nomComplet: "Tshibanda Paul", matricule: "HJ-2026-0003", ville: "Matadi", province: "Kongo-Central", etude: "Étude Tshibanda", telephone: "+243 900 555 666", statut: "Actif" },
        { nomComplet: "Nkosi Grace", matricule: "HJ-2026-0004", ville: "Goma", province: "Nord-Kivu", etude: "Étude Nkosi", telephone: "+243 900 777 888", statut: "Actif" },
        { nomComplet: "Luboya Marc", matricule: "HJ-2026-0005", ville: "Bukavu", province: "Sud-Kivu", etude: "Étude Luboya", telephone: "+243 900 999 000", statut: "Actif" },
        { nomComplet: "Kalala David", matricule: "HJ-2026-0006", ville: "Kinshasa", province: "Kinshasa", etude: "Étude Kalala", telephone: "+243 900 123 456", statut: "Actif" },
    ];

    // ===== UTILS =====
    function $(id) { return document.getElementById(id); }
    function initials(nom) {
        if (!nom) return "?";
        return nom.split(" ").filter(Boolean).map(w => w[0]).join("").slice(0, 2).toUpperCase();
    }
    function escapeHtml(s) {
        return String(s ?? "").replace(/[&<>"']/g, c => ({ "&": "&amp;", "<": "&lt;", ">": "&gt;", '"': "&quot;", "'": "&#39;" }[c]));
    }

    // ===== TABS : Huissier / Acte (page d'accueil) =====
    window.switchTab = function (tab, btn) {
        document.querySelectorAll(".tab").forEach(t => t.classList.remove("active"));
        if (btn) btn.classList.add("active");
        const h = $("tab-huissier"), a = $("tab-acte");
        if (!h || !a) return;
        h.classList.toggle("hidden", tab !== "huissier");
        a.classList.toggle("hidden", tab !== "acte");
        const hInput = h.querySelector('input[name="numero"]');
        const aInput = a.querySelector('input[name="numero"]');
        if (hInput) hInput.disabled = tab !== "huissier";
        if (aInput) aInput.disabled = tab !== "acte";
        const typeInput = document.querySelector('form input[name="type"]');
        if (typeInput) typeInput.value = tab;
    };

    // ===== VÉRIFICATION VIA API (avec fallback démo) =====
    async function verifierViaApi(type, numero) {
        try {
            const res = await fetch(`/api/verification/${type}/${encodeURIComponent(numero)}`);
            if (!res.ok) return null;
            return await res.json();
        } catch {
            return null; // API indisponible -> fallback démo
        }
    }

    window.verifierHuissier = async function () {
        const q = ($("inputHuissier")?.value || "").trim();
        const el = $("resultHuissier");
        if (!el) return;
        if (!q) { el.className = "result err"; el.textContent = "⚠️ Entrez un nom ou un matricule."; return; }
        el.className = "result"; el.textContent = "⏳ Vérification en cours..."; el.style.display = "block";

        const data = await verifierViaApi("huissier", q.toUpperCase());
        if (data) {
            el.className = "result ok";
            el.innerHTML = `✅ <strong>${escapeHtml(data.nomComplet)}</strong> est bien inscrit.<br>Matricule : <strong>${escapeHtml(data.matricule)}</strong> • ${escapeHtml(data.etude || "")} • ${escapeHtml(data.ville)} (${escapeHtml(data.province)}) • Statut : ${escapeHtml(data.statut)}`;
            return;
        }
        // Fallback démo
        const h = HUISSIERS_DEMO.find(x => x.nomComplet.toLowerCase().includes(q.toLowerCase()) || x.matricule.toLowerCase() === q.toLowerCase());
        if (h) {
            el.className = "result ok";
            el.innerHTML = `✅ <strong>${escapeHtml(h.nomComplet)}</strong> est bien inscrit.<br>Matricule : <strong>${escapeHtml(h.matricule)}</strong> • ${escapeHtml(h.etude)} • ${escapeHtml(h.ville)} (${escapeHtml(h.province)})`;
        } else {
            el.className = "result err";
            el.innerHTML = `❌ Aucun huissier trouvé pour « ${escapeHtml(q)} ».<br>Attention : il peut s'agir d'un faux huissier. Appelez le +243 900 000 000.`;
        }
    };

    window.verifierActe = async function () {
        const q = ($("inputActe")?.value || "").trim().toUpperCase();
        const el = $("resultActe");
        if (!el) return;
        if (!q) { el.className = "result err"; el.textContent = "⚠️ Entrez le numéro de l'acte."; return; }
        el.className = "result"; el.textContent = "⏳ Vérification en cours..."; el.style.display = "block";

        const data = await verifierViaApi("acte", q);
        if (data) {
            el.className = "result ok";
            el.innerHTML = `✅ Acte <strong>${escapeHtml(data.numeroActe)}</strong> authentique.<br>Type : ${escapeHtml(data.type)} • Huissier : ${escapeHtml(data.huissier)} • Destinataire : ${escapeHtml(data.destinataire)} • Statut : ${escapeHtml(data.statut)}`;
            return;
        }
        if (q.startsWith("ACT-")) {
            el.className = "result ok";
            el.innerHTML = `✅ Acte <strong>${escapeHtml(q)}</strong> authentique.<br><small>(Résultat démo — connectez l'API pour la vérification réelle)</small>`;
        } else {
            el.className = "result err";
            el.innerHTML = `❌ Format invalide. Un acte commence par <strong>ACT-</strong> (ex : ACT-2026-000123).`;
        }
    };

    // ===== LISTE HUISSIERS (page d'accueil — démo, si pas rendue par Razor) =====
    function renderHuissiers(liste) {
        const c = $("huissiersList");
        if (!c) return;
        // Si Razor a déjà rendu les cartes, on ne touche à rien
        if (c.dataset.razor === "true") return;
        if (!liste || liste.length === 0) {
            c.innerHTML = '<p style="grid-column:1/-1;text-align:center;color:#64748b;">Aucun huissier trouvé.</p>';
            return;
        }
        c.innerHTML = liste.map(h => `
            <div class="huissier-card">
                <div class="top"><span class="avatar">${initials(h.nomComplet)}</span>
                <div><strong>${escapeHtml(h.nomComplet)}</strong><br><span style="font-size:12px;color:#64748b;">${escapeHtml(h.matricule)}</span><br><span class="badge">✅ ${escapeHtml(h.statut)}</span></div></div>
                <p>📍 ${escapeHtml(h.ville)} — ${escapeHtml(h.province)}<br>🏛️ ${escapeHtml(h.etude)}<br>📞 ${escapeHtml(h.telephone)}</p>
                <div class="actions">
                    <a href="/Site/Contact" class="btn btn-dark btn-sm">Contacter</a>
                    <a href="/Site/Verifier?type=huissier&numero=${encodeURIComponent(h.matricule)}" class="btn btn-sm" style="background:#fef3c7;">Vérifier</a>
                </div>
            </div>`).join("");
    }

    window.filtrerHuissiers = function () {
        const q = ($("searchHuissier")?.value || "").toLowerCase();
        const prov = $("filterProvince")?.value || "";
        renderHuissiers(HUISSIERS_DEMO.filter(h =>
            (!q || h.nomComplet.toLowerCase().includes(q) || h.ville.toLowerCase().includes(q) || h.matricule.toLowerCase().includes(q)) &&
            (!prov || h.province === prov)
        ));
    };

    // ===== COMPTEURS ANIMÉS =====
    function animerCompteurs() {
        document.querySelectorAll("[data-count]").forEach(el => {
            const target = parseInt(el.dataset.count, 10);
            if (isNaN(target)) return;
            const t0 = performance.now();
            const duree = 1200;
            (function tick(t) {
                const p = Math.min((t - t0) / duree, 1);
                const eased = 1 - Math.pow(1 - p, 3);
                el.textContent = Math.floor(target * eased).toLocaleString("fr-FR");
                if (p < 1) requestAnimationFrame(tick);
            })(t0);
        });
    }

    // ===== MENU MOBILE : fermer au clic sur un lien =====
    function initMenu() {
        const nav = $("navMenu");
        if (!nav) return;
        nav.querySelectorAll("a").forEach(a => a.addEventListener("click", () => nav.classList.remove("open")));
        document.addEventListener("click", e => {
            const burger = e.target.closest(".burger");
            const insideNav = e.target.closest("#navMenu");
            if (!burger && !insideNav) nav.classList.remove("open");
        });
    }

    // ===== ENTRÉE = VÉRIFIER =====
    function initEnter() {
        ["inputHuissier", "inputActe"].forEach(id => {
            const el = $(id);
            if (!el) return;
            el.addEventListener("keydown", e => {
                if (e.key === "Enter") {
                    e.preventDefault();
                    if (id === "inputHuissier") verifierHuissier();
                    else verifierActe();
                }
            });
        });
    }

    // ===== INIT =====
    document.addEventListener("DOMContentLoaded", () => {
        const list = $("huissiersList");
        // Ne rend la démo que si la liste est vide ET pas rendue par Razor
        if (list && list.children.length === 0 && list.dataset.razor !== "true") {
            renderHuissiers(HUISSIERS_DEMO);
        }
        animerCompteurs();
        initMenu();
        initEnter();
    });
})();