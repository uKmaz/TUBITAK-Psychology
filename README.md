# TÜBİTAK Psychology Project: Gamified Cognitive Bias Modification 🧠🎮

> **Oyunlaştırılmış Bilişsel Yanlılık Değişiminin OKB Tanılı Bireylerde OKB Belirtileri, Obsesif İnançlar, Dikkat ve Yorumlama Yanlılıkları ve Psikolojik Sıkıntı Üzerindeki Etkisinin İncelenmesi**

This repository contains the source code for a comprehensive academic and clinical psychology project supported by TÜBİTAK. It is a gamified application designed specifically for individuals diagnosed with Obsessive-Compulsive Disorder (OCD) to study the effects of cognitive bias modification.

---

## 🔬 Project Overview

The core objective of this application is to measure and modify attention and interpretation biases in OCD patients through interactive, gamified experiences. Data is collected anonymously and sent to a secure backend for psychological and statistical analysis by researchers.

### 📅 Development Lifecycle (12 Months)
- **Phase 1: Development (Months 1-6)**
  - Gathering requirements from psychological researchers and clinicians.
  - Development of core gamified mechanics (`A-Game` & `B-Game`).
  - Integration of Firebase for real-time data collection and CSV exportation.
- **Phase 2: Maintenance & Clinical Trials (Months 7-12)**
  - Active maintenance during the clinical testing phase with real patients.
  - Bug fixes, performance optimizations, and data-flow adjustments based on researcher feedback.
  - Finalization of the project scope and reporting.

## ⚙️ Technical Architecture

- **Engine:** Unity (C#)
- **Database/Backend:** Firebase (Realtime Data Collection & CSV Export)
- **Core Modules:**
  - `GameManager.cs`: Controls the flow of the sessions, score tracking, and coordinates data export.
  - `FirebaseCSVUploader.cs` & `DataCollector`: Handles securely compiling in-game metrics (reaction times, user choices) and uploading them to Firebase in CSV format for later analysis.
  - **A-Game / B-Game**: Distinct gamified modules targeting different cognitive biases (Attention vs. Interpretation).

## 📄 Project Report

A basic development summary report (`Rapor.doc`) prepared during the process is included in the root directory. 

> **🔒 Privacy Note:** Due to KVKK (Personal Data Protection Law) and strict clinical confidentiality guidelines, all gameplay footage, UI interfaces, and data collection screens are intentionally omitted from this public repository.

## License & Copyright

&copy; Ucmaz pc. All Rights Reserved.  
This project is proprietary and intended solely for educational, academic, and portfolio purposes. It is not licensed for commercial use, distribution, or modification without explicit permission.
