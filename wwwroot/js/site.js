/* Contact Filtering Logic */
const initializeContactSearch = () => {
    const searchInput = document.querySelector("[data-contact-search]");
    const rows = Array.from(document.querySelectorAll("[data-contact-row]"));
    const emptyState = document.querySelector("[data-empty-state]");
    const counter = document.querySelector("[data-contact-count]");

    if (!searchInput || rows.length === 0) {
        return;
    }

    const refreshRows = () => {
        const query = searchInput.value.trim().toLowerCase();
        let visibleCount = 0;

        rows.forEach((row) => {
            const text = row.getAttribute("data-search") ?? "";
            const isVisible = text.includes(query);
            row.classList.toggle("is-hidden", !isVisible);

            if (isVisible) {
                visibleCount += 1;
            }
        });

        if (emptyState) {
            emptyState.classList.toggle("is-hidden", visibleCount !== 0);
        }

        if (counter) {
            counter.textContent = visibleCount.toString();
        }
    };

    searchInput.addEventListener("input", refreshRows);
    refreshRows();
};

/* UI and Feedback Logic */
const initializeAutoDismiss = () => {
    const feedbackItems = document.querySelectorAll("[data-auto-dismiss]");

    feedbackItems.forEach((item) => {
        window.setTimeout(() => {
            item.classList.add("is-hidden");
        }, 4000);
    });
};

/* Contact Modal Logic */
const initializeContactModal = () => {
    const modalElement = document.querySelector("#contactFormModal[data-open-on-load='true']");

    if (!modalElement || !window.bootstrap?.Modal) {
        return;
    }

    window.bootstrap.Modal.getOrCreateInstance(modalElement).show();
};

/* Contact Form Validation Logic */
const initializeContactFormValidation = () => {
    const forms = document.querySelectorAll("[data-contact-form]");

    const setFieldError = (input, message) => {
        const fieldName = input.getAttribute("name");
        const errorElement = fieldName
            ? document.querySelector(`[data-valmsg-for='${fieldName}']`)
            : null;

        input.classList.toggle("is-invalid", Boolean(message));
        input.setCustomValidity(message);

        if (errorElement) {
            errorElement.textContent = message;
        }
    };

    const validatePhone = (input) => {
        const value = input.value.trim();
        const digits = value.replace(/\D/g, "");
        const localDigits = digits.startsWith("55") && digits.length > 11
            ? digits.slice(2)
            : digits;
        const hasLetters = /[a-zA-Z]/.test(value);
        const isValid = !hasLetters && (localDigits.length === 10 || localDigits.length === 11);

        setFieldError(input, isValid ? "" : "Informe um telefone válido com DDD.");

        return isValid;
    };

    const allowedTopLevelDomains = new Set([
        "com",
        "org",
        "net",
        "br",
        "edu",
        "gov",
        "mil",
        "int",
        "io",
        "co",
        "info",
        "biz",
        "me",
        "app",
        "dev",
        "ai",
        "tech",
        "online",
        "site",
        "store",
        "cloud",
        "digital",
        "software",
        "systems",
        "agency",
        "company",
        "solutions",
        "services",
        "pro",
        "name",
        "tv",
        "cc",
        "us",
        "uk",
        "ca",
        "de",
        "fr",
        "es",
        "it",
        "pt",
        "ar",
        "cl",
        "mx",
        "au",
        "jp",
        "cn",
        "in"
    ]);

    const checkEmail = (emailValidator) => {
        const email = emailValidator.trim().toLowerCase();
        const regexEmail = /^[a-z0-9_%+-]+(?:\.[a-z0-9_%+-]+)*@(?:[a-z0-9](?:[a-z0-9-]{0,61}[a-z0-9])?\.)+([a-z]{2,24})$/;
        const match = regexEmail.exec(email);

        return Boolean(
            match
            && email.length <= 160
            && email.split("@")[0].length <= 64
            && allowedTopLevelDomains.has(match[1])
        );
    };

    const validateEmail = (input) => {
        const value = input.value.trim();
        const isValid = checkEmail(value);

        setFieldError(input, isValid ? "" : "Informe um e-mail válido.");

        return isValid;
    };

    forms.forEach((form) => {
        const phoneInput = form.querySelector("[data-phone-input]");
        const emailInput = form.querySelector("[data-email-input]");

        phoneInput?.addEventListener("input", () => validatePhone(phoneInput));
        emailInput?.addEventListener("input", () => validateEmail(emailInput));

        form.addEventListener("submit", (event) => {
            const isPhoneValid = phoneInput ? validatePhone(phoneInput) : true;
            const isEmailValid = emailInput ? validateEmail(emailInput) : true;

            if (!isPhoneValid || !isEmailValid) {
                event.preventDefault();
                event.stopPropagation();
            }
        });
    });
};

/* Navigation and Mobile Menu Logic */
const initializeMobileMenu = () => {
    const toggle = document.querySelector("[data-menu-toggle]");
    const nav = document.querySelector(".site-nav");

    if (!toggle || !nav) {
        return;
    }

    const setMenuState = (isOpen) => {
        toggle.setAttribute("aria-expanded", isOpen.toString());
        toggle.setAttribute("aria-label", isOpen ? "Fechar menu" : "Abrir menu");
        nav.classList.toggle("is-open", isOpen);
    };

    toggle.addEventListener("click", () => {
        const isOpen = toggle.getAttribute("aria-expanded") === "true";
        setMenuState(!isOpen);
    });

    nav.addEventListener("click", (event) => {
        if (event.target.closest(".site-nav-link")) {
            setMenuState(false);
        }
    });

    window.addEventListener("resize", () => {
        if (window.matchMedia("(min-width: 641px)").matches) {
            setMenuState(false);
        }
    });
};

/* Global Initialization */
document.addEventListener("DOMContentLoaded", () => {
    initializeContactSearch();
    initializeAutoDismiss();
    initializeContactModal();
    initializeContactFormValidation();
    initializeMobileMenu();
});
