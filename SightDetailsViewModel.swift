//
//  SightDetailsViewModel.swift
//  Escapani
//
//  Created by Panos Peltekis on 13/4/21.
//

import Foundation

protocol SightDetailsViewModelInput {}

protocol SightDetailsViewModelOutput {
    var sight: DigitalWalkSightType { get set }
}

protocol SightDetailsViewModel: SightDetailsViewModelInput, SightDetailsViewModelOutput, BaseViewModel {}

class DefaultSightDetailsViewModel: BaseViewModel, SightDetailsViewModel {
    
    // MARK: - Properties
    
    var sight: DigitalWalkSightType
    
    // MARK: - Init
    
    init(theme: MainTheme, sight: DigitalWalkSightType) {
        self.sight = sight
        super.init(theme: theme)
    }
}
