-- MySQL dump 10.13  Distrib 8.0.31, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: rol
-- ------------------------------------------------------
-- Server version	8.0.31

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `rasgos`
--

DROP TABLE IF EXISTS `rasgos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rasgos` (
  `Nombre` varchar(70) DEFAULT NULL,
  `indice` varchar(70) NOT NULL,
  PRIMARY KEY (`indice`),
  KEY `inde` (`Nombre`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rasgos`
--

LOCK TABLES `rasgos` WRITE;
/*!40000 ALTER TABLE `rasgos` DISABLE KEYS */;
INSERT INTO `rasgos` VALUES ('Ability Score Improvement','/api/features/barbarian-ability-score-improvement-1'),('Ability Score Improvement','/api/features/barbarian-ability-score-improvement-2'),('Ability Score Improvement','/api/features/barbarian-ability-score-improvement-3'),('Ability Score Improvement','/api/features/barbarian-ability-score-improvement-4'),('Ability Score Improvement','/api/features/barbarian-ability-score-improvement-5'),('Ability Score Improvement','/api/features/bard-ability-score-improvement-1'),('Ability Score Improvement','/api/features/bard-ability-score-improvement-2'),('Ability Score Improvement','/api/features/bard-ability-score-improvement-3'),('Ability Score Improvement','/api/features/bard-ability-score-improvement-4'),('Ability Score Improvement','/api/features/bard-ability-score-improvement-5'),('Ability Score Improvement','/api/features/cleric-ability-score-improvement-1'),('Ability Score Improvement','/api/features/cleric-ability-score-improvement-2'),('Ability Score Improvement','/api/features/cleric-ability-score-improvement-3'),('Ability Score Improvement','/api/features/cleric-ability-score-improvement-4'),('Ability Score Improvement','/api/features/cleric-ability-score-improvement-5'),('Ability Score Improvement','/api/features/druid-ability-score-improvement-1'),('Ability Score Improvement','/api/features/druid-ability-score-improvement-2'),('Ability Score Improvement','/api/features/druid-ability-score-improvement-3'),('Ability Score Improvement','/api/features/druid-ability-score-improvement-4'),('Ability Score Improvement','/api/features/druid-ability-score-improvement-5'),('Ability Score Improvement','/api/features/fighter-ability-score-improvement-1'),('Ability Score Improvement','/api/features/fighter-ability-score-improvement-2'),('Ability Score Improvement','/api/features/fighter-ability-score-improvement-3'),('Ability Score Improvement','/api/features/fighter-ability-score-improvement-4'),('Ability Score Improvement','/api/features/fighter-ability-score-improvement-5'),('Ability Score Improvement','/api/features/fighter-ability-score-improvement-6'),('Ability Score Improvement','/api/features/fighter-ability-score-improvement-7'),('Ability Score Improvement','/api/features/monk-ability-score-improvement-1'),('Ability Score Improvement','/api/features/monk-ability-score-improvement-2'),('Ability Score Improvement','/api/features/monk-ability-score-improvement-3'),('Ability Score Improvement','/api/features/monk-ability-score-improvement-4'),('Ability Score Improvement','/api/features/monk-ability-score-improvement-5'),('Ability Score Improvement','/api/features/paladin-ability-score-improvement-1'),('Ability Score Improvement','/api/features/paladin-ability-score-improvement-2'),('Ability Score Improvement','/api/features/paladin-ability-score-improvement-3'),('Ability Score Improvement','/api/features/paladin-ability-score-improvement-4'),('Ability Score Improvement','/api/features/paladin-ability-score-improvement-5'),('Ability Score Improvement','/api/features/ranger-ability-score-improvement-1'),('Ability Score Improvement','/api/features/ranger-ability-score-improvement-2'),('Ability Score Improvement','/api/features/ranger-ability-score-improvement-3'),('Ability Score Improvement','/api/features/ranger-ability-score-improvement-4'),('Ability Score Improvement','/api/features/ranger-ability-score-improvement-5'),('Ability Score Improvement','/api/features/rogue-ability-score-improvement-1'),('Ability Score Improvement','/api/features/rogue-ability-score-improvement-2'),('Ability Score Improvement','/api/features/rogue-ability-score-improvement-3'),('Ability Score Improvement','/api/features/rogue-ability-score-improvement-4'),('Ability Score Improvement','/api/features/rogue-ability-score-improvement-5'),('Ability Score Improvement','/api/features/rogue-ability-score-improvement-6'),('Ability Score Improvement','/api/features/sorcerer-ability-score-improvement-1'),('Ability Score Improvement','/api/features/sorcerer-ability-score-improvement-2'),('Ability Score Improvement','/api/features/sorcerer-ability-score-improvement-3'),('Ability Score Improvement','/api/features/sorcerer-ability-score-improvement-4'),('Ability Score Improvement','/api/features/sorcerer-ability-score-improvement-5'),('Ability Score Improvement','/api/features/warlock-ability-score-improvement-1'),('Ability Score Improvement','/api/features/warlock-ability-score-improvement-2'),('Ability Score Improvement','/api/features/warlock-ability-score-improvement-3'),('Ability Score Improvement','/api/features/warlock-ability-score-improvement-4'),('Ability Score Improvement','/api/features/warlock-ability-score-improvement-5'),('Ability Score Improvement','/api/features/wizard-ability-score-improvement-1'),('Ability Score Improvement','/api/features/wizard-ability-score-improvement-2'),('Ability Score Improvement','/api/features/wizard-ability-score-improvement-3'),('Ability Score Improvement','/api/features/wizard-ability-score-improvement-4'),('Ability Score Improvement','/api/features/wizard-ability-score-improvement-5'),('Action Surge (1 use)','/api/features/action-surge-1-use'),('Action Surge (2 uses)','/api/features/action-surge-2-uses'),('Additional Fighting Style','/api/features/additional-fighting-style'),('Additional Magical Secrets','/api/features/additional-magical-secrets'),('Arcane Recovery','/api/features/arcane-recovery'),('Arcane Tradition','/api/features/arcane-tradition'),('Archdruid','/api/features/archdruid'),('Artificer\'s Lore','/api/traits/artificers-lore'),('Aura improvements','/api/features/aura-improvements'),('Aura of Courage','/api/features/aura-of-courage'),('Aura of Devotion','/api/features/aura-of-devotion'),('Aura of Protection','/api/features/aura-of-protection'),('Bard College','/api/features/bard-college'),('Bardic Inspiration (d10)','/api/features/bardic-inspiration-d10'),('Bardic Inspiration (d12)','/api/features/bardic-inspiration-d12'),('Bardic Inspiration (d6)','/api/features/bardic-inspiration-d6'),('Bardic Inspiration (d8)','/api/features/bardic-inspiration-d8'),('Beast Spells','/api/features/beast-spells'),('Blessed Healer','/api/features/blessed-healer'),('Blindsense','/api/features/blindsense'),('Bonus Cantrip','/api/features/bonus-cantrip'),('Bonus Proficiencies','/api/features/bonus-proficiencies'),('Bonus Proficiency','/api/features/bonus-proficiency'),('Brave','/api/traits/brave'),('Breath Weapon','/api/traits/breath-weapon'),('Brutal Critical (1 die)','/api/features/brutal-critical-1-die'),('Brutal Critical (2 dice)','/api/features/brutal-critical-2-dice'),('Brutal Critical (3 dice)','/api/features/brutal-critical-3-dice'),('Channel Divinity','/api/features/channel-divinity'),('Channel Divinity (1/rest)','/api/features/channel-divinity-1-rest'),('Channel Divinity (2/rest)','/api/features/channel-divinity-2-rest'),('Channel Divinity (3/rest)','/api/features/channel-divinity-3-rest'),('Channel Divinity: Preserve Life','/api/features/channel-divinity-preserve-life'),('Channel Divinity: Sacred Weapon','/api/features/channel-divinity-sacred-weapon'),('Channel Divinity: Turn the Unholy','/api/features/channel-divinity-turn-the-unholy'),('Channel Divinity: Turn Undead','/api/features/channel-divinity-turn-undead'),('Circle of the Land','/api/features/circle-of-the-land'),('Circle of the Land: Arctic','/api/features/circle-of-the-land-arctic'),('Circle of the Land: Coast','/api/features/circle-of-the-land-coast'),('Circle of the Land: Desert','/api/features/circle-of-the-land-desert'),('Circle of the Land: Forest','/api/features/circle-of-the-land-forest'),('Circle of the Land: Grassland','/api/features/circle-of-the-land-grassland'),('Circle of the Land: Mountain','/api/features/circle-of-the-land-mountain'),('Circle of the Land: Swamp','/api/features/circle-of-the-land-swamp'),('Circle Spells','/api/features/circle-spells-1'),('Circle Spells','/api/features/circle-spells-2'),('Circle Spells','/api/features/circle-spells-3'),('Circle Spells','/api/features/circle-spells-4'),('Cleansing Touch','/api/features/cleansing-touch'),('Countercharm','/api/features/countercharm'),('Cunning Action','/api/features/cunning-action'),('Cutting Words','/api/features/cutting-words'),('Damage Resistance','/api/traits/damage-resistance'),('Danger Sense','/api/features/danger-sense'),('Dark One\'s Blessing','/api/features/dark-ones-blessing'),('Dark One\'s Own Luck','/api/features/dark-ones-own-luck'),('Darkvision','/api/traits/darkvision'),('Defensive Tactics','/api/features/defensive-tactics'),('Defensive Tactics: Escape the Horde','/api/features/defensive-tactics-escape-the-horde'),('Defensive Tactics: Multiattack Defense','/api/features/defensive-tactics-multiattack-defense'),('Defensive Tactics: Steel Will','/api/features/defensive-tactics-steel-will'),('Deflect Missiles','/api/features/deflect-missiles'),('Destroy Undead (CR 1 or below)','/api/features/destroy-undead-cr-1-or-below'),('Destroy Undead (CR 1/2 or below)','/api/features/destroy-undead-cr-1-2-or-below'),('Destroy Undead (CR 2 or below)','/api/features/destroy-undead-cr-2-or-below'),('Destroy Undead (CR 3 or below)','/api/features/destroy-undead-cr-3-or-below'),('Destroy Undead (CR 4 or below)','/api/features/destroy-undead-cr-4-or-below'),('Diamond Soul','/api/features/diamond-soul'),('Disciple of Life','/api/features/disciple-of-life'),('Divine Domain','/api/features/divine-domain'),('Divine Health','/api/features/divine-health'),('Divine Intervention','/api/features/divine-intervention'),('Divine Intervention Improvement','/api/features/divine-intervention-improvement'),('Divine Sense','/api/features/divine-sense'),('Divine Smite','/api/features/divine-smite'),('Divine Strike','/api/features/divine-strike'),('Domain Spells','/api/features/domain-spells-1'),('Domain Spells','/api/features/domain-spells-2'),('Domain Spells','/api/features/domain-spells-3'),('Domain Spells','/api/features/domain-spells-4'),('Domain Spells','/api/features/domain-spells-5'),('Draconic Ancestry','/api/traits/draconic-ancestry'),('Draconic Ancestry (Black)','/api/traits/draconic-ancestry-black'),('Draconic Ancestry (Blue)','/api/traits/draconic-ancestry-blue'),('Draconic Ancestry (Brass)','/api/traits/draconic-ancestry-brass'),('Draconic Ancestry (Bronze)','/api/traits/draconic-ancestry-bronze'),('Draconic Ancestry (Copper)','/api/traits/draconic-ancestry-copper'),('Draconic Ancestry (Gold)','/api/traits/draconic-ancestry-gold'),('Draconic Ancestry (Green)','/api/traits/draconic-ancestry-green'),('Draconic Ancestry (Red)','/api/traits/draconic-ancestry-red'),('Draconic Ancestry (Silver)','/api/traits/draconic-ancestry-silver'),('Draconic Ancestry (White)','/api/traits/draconic-ancestry-white'),('Draconic Presence','/api/features/draconic-presence'),('Draconic Resilience','/api/features/draconic-resilience'),('Dragon Ancestor','/api/features/dragon-ancestor'),('Dragon Ancestor: Black - Acid Damage','/api/features/dragon-ancestor-black---acid-damage'),('Dragon Ancestor: Blue - Lightning Damage','/api/features/dragon-ancestor-blue---lightning-damage'),('Dragon Ancestor: Brass - Fire Damage','/api/features/dragon-ancestor-brass---fire-damage'),('Dragon Ancestor: Bronze - Lightning Damage','/api/features/dragon-ancestor-bronze---lightning-damage'),('Dragon Ancestor: Copper - Acid Damage','/api/features/dragon-ancestor-copper---acid-damage'),('Dragon Ancestor: Gold - Fire Damage','/api/features/dragon-ancestor-gold---fire-damage'),('Dragon Ancestor: Green - Poison Damage','/api/features/dragon-ancestor-green---poison-damage'),('Dragon Ancestor: Red - Fire Damage','/api/features/dragon-ancestor-red---fire-damage'),('Dragon Ancestor: Silver - Cold Damage','/api/features/dragon-ancestor-silver---cold-damage'),('Dragon Ancestor: White - Cold Damage','/api/features/dragon-ancestor-white---cold-damage'),('Dragon Wings','/api/features/dragon-wings'),('Druid Circle','/api/features/druid-circle'),('Druidic','/api/features/druidic'),('Dwarven Combat Training','/api/traits/dwarven-combat-training'),('Dwarven Resilience','/api/traits/dwarven-resilience'),('Dwarven Toughness','/api/traits/dwarven-toughness'),('Eldritch Invocation: Agonizing Blast','/api/features/eldritch-invocation-agonizing-blast'),('Eldritch Invocation: Armor of Shadows','/api/features/eldritch-invocation-armor-of-shadows'),('Eldritch Invocation: Ascendant Step','/api/features/eldritch-invocation-ascendant-step'),('Eldritch Invocation: Beast Speech','/api/features/eldritch-invocation-beast-speech'),('Eldritch Invocation: Beguiling Influence','/api/features/eldritch-invocation-beguiling-influence'),('Eldritch Invocation: Bewitching Whispers','/api/features/eldritch-invocation-bewitching-whispers'),('Eldritch Invocation: Book of Ancient Secrets','/api/features/eldritch-invocation-book-of-ancient-secrets'),('Eldritch Invocation: Chains of Carceri','/api/features/eldritch-invocation-chains-of-carceri'),('Eldritch Invocation: Devil\'s Sight','/api/features/eldritch-invocation-devils-sight'),('Eldritch Invocation: Dreadful Word','/api/features/eldritch-invocation-dreadful-word'),('Eldritch Invocation: Eldritch Sight','/api/features/eldritch-invocation-eldritch-sight'),('Eldritch Invocation: Eldritch Spear','/api/features/eldritch-invocation-eldritch-spear'),('Eldritch Invocation: Eyes of the Rune Keeper','/api/features/eldritch-invocation-eyes-of-the-rune-keeper'),('Eldritch Invocation: Fiendish Vigor','/api/features/eldritch-invocation-fiendish-vigor'),('Eldritch Invocation: Gaze of Two Minds','/api/features/eldritch-invocation-gaze-of-two-minds'),('Eldritch Invocation: Lifedrinker','/api/features/eldritch-invocation-lifedrinker'),('Eldritch Invocation: Mask of Many Faces','/api/features/eldritch-invocation-mask-of-many-faces'),('Eldritch Invocation: Master of Myriad Forms','/api/features/eldritch-invocation-master-of-myriad-forms'),('Eldritch Invocation: Minions of Chaos','/api/features/eldritch-invocation-minions-of-chaos'),('Eldritch Invocation: Mire the Mind','/api/features/eldritch-invocation-mire-the-mind'),('Eldritch Invocation: Misty Visions','/api/features/eldritch-invocation-misty-visions'),('Eldritch Invocation: One with Shadows','/api/features/eldritch-invocation-one-with-shadows'),('Eldritch Invocation: Otherworldly Leap','/api/features/eldritch-invocation-otherworldly-leap'),('Eldritch Invocation: Repelling Blast','/api/features/eldritch-invocation-repelling-blast'),('Eldritch Invocation: Sculptor of Flesh','/api/features/eldritch-invocation-sculptor-of-flesh'),('Eldritch Invocation: Sign of Ill Omen','/api/features/eldritch-invocation-sign-of-ill-omen'),('Eldritch Invocation: Thief of Five Fates','/api/features/eldritch-invocation-thief-of-five-fates'),('Eldritch Invocation: Thirsting Blade','/api/features/eldritch-invocation-thirsting-blade'),('Eldritch Invocation: Visions of Distant Realms','/api/features/eldritch-invocation-visions-of-distant-realms'),('Eldritch Invocation: Voice of the Chain Master','/api/features/eldritch-invocation-voice-of-the-chain-master'),('Eldritch Invocation: Whispers of the Grave','/api/features/eldritch-invocation-whispers-of-the-grave'),('Eldritch Invocation: Witch Sight','/api/features/eldritch-invocation-witch-sight'),('Eldritch Invocations','/api/features/eldritch-invocations'),('Eldritch Master','/api/features/eldritch-master'),('Elemental Affinity','/api/features/elemental-affinity'),('Elf Weapon Training','/api/traits/elf-weapon-training'),('Elusive','/api/features/elusive'),('Empowered Evocation','/api/features/empowered-evocation'),('Empty Body','/api/features/empty-body'),('Evasion','/api/features/monk-evasion'),('Evasion','/api/features/rogue-evasion'),('Evocation Savant','/api/features/evocation-savant'),('Expertise','/api/features/bard-expertise-1'),('Expertise','/api/features/bard-expertise-2'),('Expertise','/api/features/rogue-expertise-1'),('Expertise','/api/features/rogue-expertise-2'),('Extra Attack','/api/features/barbarian-extra-attack'),('Extra Attack','/api/features/extra-attack-1'),('Extra Attack','/api/features/monk-extra-attack'),('Extra Attack','/api/features/paladin-extra-attack'),('Extra Attack','/api/features/ranger-extra-attack'),('Extra Attack (2)','/api/features/extra-attack-2'),('Extra Attack (3)','/api/features/extra-attack-3'),('Extra Language','/api/traits/extra-language'),('Fast Hands','/api/features/fast-hands'),('Fast Movement','/api/features/fast-movement'),('Favored Enemy (1 type)','/api/features/favored-enemy-1-type'),('Favored Enemy (2 types)','/api/features/favored-enemy-2-types'),('Favored Enemy (3 enemies)','/api/features/favored-enemy-3-enemies'),('Feral Instinct','/api/features/feral-instinct'),('Feral Senses','/api/features/feral-senses'),('Fey Ancestry','/api/traits/fey-ancestry'),('Fiendish Resilience','/api/features/fiendish-resilience'),('Fighting Style','/api/features/fighter-fighting-style'),('Fighting Style','/api/features/paladin-fighting-style'),('Fighting Style','/api/features/ranger-fighting-style'),('Fighting Style: Archery','/api/features/fighter-fighting-style-archery'),('Fighting Style: Archery','/api/features/ranger-fighting-style-archery'),('Fighting Style: Defense','/api/features/fighter-fighting-style-defense'),('Fighting Style: Defense','/api/features/fighting-style-defense'),('Fighting Style: Defense','/api/features/ranger-fighting-style-defense'),('Fighting Style: Dueling','/api/features/fighter-fighting-style-dueling'),('Fighting Style: Dueling','/api/features/fighting-style-dueling'),('Fighting Style: Dueling','/api/features/ranger-fighting-style-dueling'),('Fighting Style: Great Weapon Fighting','/api/features/fighter-fighting-style-great-weapon-fighting'),('Fighting Style: Great Weapon Fighting','/api/features/fighting-style-great-weapon-fighting'),('Fighting Style: Protection','/api/features/fighter-fighting-style-protection'),('Fighting Style: Protection','/api/features/fighting-style-protection'),('Fighting Style: Two-Weapon Fighting','/api/features/fighter-fighting-style-two-weapon-fighting'),('Fighting Style: Two-Weapon Fighting','/api/features/ranger-fighting-style-two-weapon-fighting'),('Flexible Casting: Converting Spell Slot','/api/features/flexible-casting-converting-spell-slot'),('Flexible Casting: Creating Spell Slots','/api/features/flexible-casting-creating-spell-slots'),('Flurry of Blows','/api/features/flurry-of-blows'),('Foe Slayer','/api/features/foe-slayer'),('Font of Inspiration','/api/features/font-of-inspiration'),('Font of Magic','/api/features/font-of-magic'),('Frenzy','/api/features/frenzy'),('Gnome Cunning','/api/traits/gnome-cunning'),('Halfling Nimbleness','/api/traits/halfling-nimbleness'),('Hellish Resistance','/api/traits/hellish-resistance'),('Hide in Plain Sight','/api/features/hide-in-plain-sight'),('High Elf Cantrip','/api/traits/high-elf-cantrip'),('Holy Nimbus','/api/features/holy-nimbus'),('Hunter\'s Prey','/api/features/hunters-prey'),('Hunter\'s Prey: Colossus Slayer','/api/features/hunters-prey-colossus-slayer'),('Hunter\'s Prey: Giant Killer','/api/features/hunters-prey-giant-killer'),('Hunter\'s Prey: Horde Breaker','/api/features/hunters-prey-horde-breaker'),('Hurl Through Hell','/api/features/hurl-through-hell'),('Improved Critical','/api/features/improved-critical'),('Improved Divine Smite','/api/features/improved-divine-smite'),('Indomitable (1 use)','/api/features/indomitable-1-use'),('Indomitable (2 uses)','/api/features/indomitable-2-uses'),('Indomitable (3 uses)','/api/features/indomitable-3-uses'),('Indomitable Might','/api/features/indomitable-might'),('Infernal Legacy','/api/traits/infernal-legacy'),('Intimidating Presence','/api/features/intimidating-presence'),('Jack of All Trades','/api/features/jack-of-all-trades'),('Keen Senses','/api/traits/keen-senses'),('Ki','/api/features/ki'),('Ki Empowered Strikes','/api/features/ki-empowered-strikes'),('Land\'s Stride','/api/features/druid-lands-stride'),('Land\'s Stride','/api/features/ranger-lands-stride'),('Lay on Hands','/api/features/lay-on-hands'),('Lucky','/api/traits/lucky'),('Magical Secrets','/api/features/magical-secrets-1'),('Magical Secrets','/api/features/magical-secrets-2'),('Magical Secrets','/api/features/magical-secrets-3'),('Martial Archetype','/api/features/martial-archetype'),('Martial Arts','/api/features/martial-arts'),('Menacing','/api/traits/menacing'),('Metamagic','/api/features/metamagic-1'),('Metamagic','/api/features/metamagic-2'),('Metamagic','/api/features/metamagic-3'),('Metamagic: Careful Spell','/api/features/metamagic-careful-spell'),('Metamagic: Distant Spell','/api/features/metamagic-distant-spell'),('Metamagic: Empowered Spell','/api/features/metamagic-empowered-spell'),('Metamagic: Extended Spell','/api/features/metamagic-extended-spell'),('Metamagic: Heightened Spell','/api/features/metamagic-heightened-spell'),('Metamagic: Quickened Spell','/api/features/metamagic-quickened-spell'),('Metamagic: Subtle Spell','/api/features/metamagic-subtle-spell'),('Metamagic: Twinned Spell','/api/features/metamagic-twinned-spell'),('Mindless Rage','/api/features/mindless-rage'),('Monastic Tradition','/api/features/monastic-tradition'),('Multiattack','/api/features/multiattack'),('Multiattack: Volley','/api/features/multiattack-volley'),('Multiattack: Whirlwind Attack','/api/features/multiattack-whirlwind-attack'),('Mystic Arcanum (6th level)','/api/features/mystic-arcanum-6th-level'),('Mystic Arcanum (7th level)','/api/features/mystic-arcanum-7th-level'),('Mystic Arcanum (8th level)','/api/features/mystic-arcanum-8th-level'),('Mystic Arcanum (9th level)','/api/features/mystic-arcanum-9th-level'),('Natural Explorer (1 terrain type)','/api/features/natural-explorer-1-terrain-type'),('Natural Explorer (2 terrain types)','/api/features/natural-explorer-2-terrain-types'),('Natural Explorer (3 terrain types)','/api/features/natural-explorer-3-terrain-types'),('Natural Recovery','/api/features/natural-recovery'),('Naturally Stealthy','/api/traits/naturally-stealthy'),('Nature\'s Sanctuary','/api/features/natures-sanctuary'),('Nature\'s Ward','/api/features/natures-ward'),('Oath Spells','/api/features/oath-spells'),('Open Hand Technique','/api/features/open-hand-technique'),('Otherworldly Patron','/api/features/otherworldly-patron'),('Overchannel','/api/features/overchannel'),('Pact Boon','/api/features/pact-boon'),('Pact Magic','/api/features/pact-magic'),('Pact of the Blade','/api/features/pact-of-the-blade'),('Pact of the Chain','/api/features/pact-of-the-chain'),('Pact of the Tome','/api/features/pact-of-the-tome'),('Patient Defense','/api/features/patient-defense'),('Peerless Skill','/api/features/peerless-skill'),('Perfect Self','/api/features/perfect-self'),('Persistent Rage','/api/features/persistent-rage'),('Potent Cantrip','/api/features/potent-cantrip'),('Primal Champion','/api/features/primal-champion'),('Primal Path','/api/features/primal-path'),('Primeval Awareness','/api/features/primeval-awareness'),('Purity of Body','/api/features/purity-of-body'),('Purity of Spirit','/api/features/purity-of-spirit'),('Quivering Palm','/api/features/quivering-palm'),('Rage','/api/features/rage'),('Ranger Archetype','/api/features/ranger-archetype'),('Reckless Attack','/api/features/reckless-attack'),('Relentless Endurance','/api/traits/relentless-endurance'),('Relentless Rage','/api/features/relentless-rage'),('Reliable Talent','/api/features/reliable-talent'),('Remarkable Athlete','/api/features/remarkable-athlete'),('Retaliation','/api/features/retaliation'),('Roguish Archetype','/api/features/roguish-archetype'),('Sacred Oath','/api/features/sacred-oath'),('Savage Attacks','/api/traits/savage-attacks'),('Sculpt Spells','/api/features/sculpt-spells'),('Second Wind','/api/features/second-wind'),('Second-Story Work','/api/features/second-story-work'),('Signature Spell','/api/features/signature-spell'),('Skill Versatility','/api/traits/skill-versatility'),('Slippery Mind','/api/features/slippery-mind'),('Slow Fall','/api/features/slow-fall'),('Sneak Attack','/api/features/sneak-attack'),('Song of Rest (d10)','/api/features/song-of-rest-d10'),('Song of Rest (d12)','/api/features/song-of-rest-d12'),('Song of Rest (d6)','/api/features/song-of-rest-d6'),('Song of Rest (d8)','/api/features/song-of-rest-d8'),('Sorcerous Origin','/api/features/sorcerous-origin'),('Sorcerous Restoration','/api/features/sorcerous-restoration'),('Spell Mastery','/api/features/spell-mastery'),('Spellcasting: Bard','/api/features/spellcasting-bard'),('Spellcasting: Cleric','/api/features/spellcasting-cleric'),('Spellcasting: Druid','/api/features/spellcasting-druid'),('Spellcasting: Paladin','/api/features/spellcasting-paladin'),('Spellcasting: Ranger','/api/features/spellcasting-ranger'),('Spellcasting: Sorcerer','/api/features/spellcasting-sorcerer'),('Spellcasting: Wizard','/api/features/spellcasting-wizard'),('Step of the Wind','/api/features/step-of-the-wind'),('Stillness of Mind','/api/features/stillness-of-mind'),('Stonecunning','/api/traits/stonecunning'),('Stroke of Luck','/api/features/stroke-of-luck'),('Stunning Strike','/api/features/stunning-strike'),('Superior Critical','/api/features/superior-critical'),('Superior Hunter\'s Defense','/api/features/superior-hunters-defense'),('Superior Hunter\'s Defense: Evasion','/api/features/superior-hunters-defense-evasion'),('Superior Hunter\'s Defense: Stand Against the Tide','/api/features/superior-hunters-defense-stand-against-the-tide'),('Superior Hunter\'s Defense: Uncanny Dodge','/api/features/superior-hunters-defense-uncanny-dodge'),('Superior Inspiration','/api/features/superior-inspiration'),('Supreme Healing','/api/features/supreme-healing'),('Supreme Sneak','/api/features/supreme-sneak'),('Survivor','/api/features/survivor'),('Thief\'s Reflexes','/api/features/thiefs-reflexes'),('Thieves\' Cant','/api/features/thieves-cant'),('Timeless Body','/api/features/druid-timeless-body'),('Timeless Body','/api/features/monk-timeless-body'),('Tinker','/api/traits/tinker'),('Tongue of the Sun and Moon','/api/features/tongue-of-the-sun-and-moon'),('Tool Proficiency','/api/traits/tool-proficiency'),('Trance','/api/traits/trance'),('Tranquility','/api/features/tranquility'),('Unarmored Defense','/api/features/barbarian-unarmored-defense'),('Unarmored Defense','/api/features/monk-unarmored-defense'),('Unarmored Movement','/api/features/unarmored-movement-1'),('Unarmored Movement','/api/features/unarmored-movement-2'),('Uncanny Dodge','/api/features/uncanny-dodge'),('Use Magic Device','/api/features/use-magic-device'),('Vanish','/api/features/vanish'),('Wholeness of Body','/api/features/wholeness-of-body'),('Wild Shape (CR 1 or below)','/api/features/wild-shape-cr-1-or-below'),('Wild Shape (CR 1/2 or below, no flying speed)','/api/features/wild-shape-cr-1-2-or-below-no-flying-speed'),('Wild Shape (CR 1/4 or below, no flying or swim speed)','/api/features/wild-shape-cr-1-4-or-below-no-flying-or-swim-speed');
/*!40000 ALTER TABLE `rasgos` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-06-06  9:57:40