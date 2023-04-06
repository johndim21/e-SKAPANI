package app.e_skapani.ui.wizard_ar.model

import app.e_skapani.R

enum class WizaArUiItem(
    val listOfImages: IntArray,
    val listOfDescription: IntArray,
    val modelFileName: Int
) {
    KAMARA(
        intArrayOf(
            R.drawable.ic_bubble_kamara,
            R.drawable.ic_kamara
        ),
        intArrayOf(
            R.string.wizard_ar_walk,
            R.string.wizard_gizmo
        ),
        R.string.arch_of_galerius_model_name
    ),
    IPPODROMOS( intArrayOf(
        R.drawable.ic_bubble_ippodromos,
        R.drawable.ic_ippodromos
    ),
        intArrayOf(
            R.string.wizard_ar_walk,
            R.string.wizard_gizmo
        ),
        R.string.hippodrome_model_name
    ),
    OKTAGONO( intArrayOf(
        R.drawable.ic_bubble_oktagwno,
        R.drawable.ic_oktagwno
    ),
        intArrayOf(
            R.string.wizard_ar_walk,
            R.string.wizard_gizmo
        ),
        R.string.octagon_model_name
    ),

    VASILIKI(
        intArrayOf(
            R.drawable.ic_bubble_vasiliki,
            R.drawable.ic_vasiliki
        ),
        intArrayOf(
            R.string.wizard_ar_walk,
            R.string.wizard_gizmo
        ),
        R.string.vasiliki_model_name
    ),
    MOUSEIO(
        intArrayOf(
            R.drawable.ic_bubble_toxo_tou_galeriou
        ),
        intArrayOf(
            R.string.wizard_ar_inside_museum
        ),
        R.string.museum_ar_model_name
    ),
}