namespace HuissariatPro.Models;

public static class LEnum
{
    public enum Sexe { Homme, Femme }
    public enum StatutHuissier { Actif, Suspendu, Retraite, Decede, Radie }
    public enum TypeClient { Particulier, Entreprise, Avocat, Tribunal, Administration }
    public enum TypeDossier { Civil, Penal, Commercial, Familial, Foncier, Recouvrement, Saisie, Expulsion, Constat }
    public enum StatutDossier { EnCours, EnAttente, Suspendu, Cloture, Archive }
    public enum TypeActe { Signification, Citation, Commandement, Saisie, Constat, ProcesVerbal, Inventaire, Expulsion, Assignation, Notification }
    public enum StatutActe { Redige, Signifie, NonSignifie, Annule }
    public enum ModePaiement { Especes, Virement, MobileMoney, Cheque, Carte }
    public enum StatutPaiement { EnAttente, Paye, Partiel, Annule }
    public enum TypeAudience { Audience, Delibere, Plaidoirie, Conciliation, Expertise }
}