# Tower Defence Core

**Tower Defence Core** — это экспериментальный проект, реализующий механику игры в жанре Tower Defense с использованием архитектуры ECS (Entity Component System) в Unity. Проект демонстрирует современные подходы к разработке игр с акцентом на производительность и масштабируемость.

---

## Демонстрация:

![GamePlay](https://github.com/SweetJS64/untity-td-core/blob/main/Docs/shop.gif)

![GamePlay](https://github.com/SweetJS64/untity-td-core/blob/main/Docs/victoryGame.gif)

![GamePlay](https://github.com/SweetJS64/untity-td-core/blob/main/Docs/defeatGame.gif)

---

## Что реализовано

- **ECS-архитектура**: Использование Unity DOTS для эффективного управления сущностями и компонентами.
- **Система урона**: Реализация различных эффектов урона, включая горение, с использованием IBufferElementData и ScriptableObject.
- **UI-интерфейс**: Интеграция пользовательского интерфейса для отображения состояния игры и управления.
- **Магазин башен**: Игрок может приобретать башни с разными характеристиками, описанными через ScriptableObject.
- **Сетка установки**: Башни можно размещать только в доступные ячейки сетки, что исключает ошибки размещения.

---

## Технологии и инструменты

- **Unity 2022.3+**
- **Unity DOTS (Entities 1.0.0-pre.3)**
- **Burst Compiler**
- **Jobs System**
- **ScriptableObject**
- **EntityCommandBuffer**

---

## Как запустить:

1. **Клонируйте репозиторий**
2. **Откройте проект в Unity 2022.3.13f1**
3. **Запустите сцену `LevelScene` для начала игры** 

---

## Истрия проекта:

С историей коммитов можно ознакомиться тут: [frogsrop/TowerDefenceCore](https://github.com/frogsrop/TowerDefenceCore).
